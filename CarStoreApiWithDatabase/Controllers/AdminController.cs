using CarStoreApi.Data;
using CarStoreApi.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace CarStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(DatabaseContext _db) : ControllerBase
    {
        

        [HttpPost("Login")]
        
        public ActionResult<UserLoginDTO> Login(RequestLoginDto userdto)
        {
            if (string.IsNullOrEmpty(userdto.name) || string.IsNullOrEmpty(userdto.password))
            {
                return BadRequest("Invalid UserName");
            }
            if (string.IsNullOrEmpty(userdto.password))
            {
                return BadRequest("Invalid Password");
            }


            User user = _db.Users.FirstOrDefault(u => u.UserName == userdto.name);
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            if(!user.VerifyPassword(userdto.password)) {
                return BadRequest("Invalid password");
            }

            
            string role = user.UserName == "amin" ? "Admin" : "User";

            
            Response.Cookies.Append("Username", userdto.name, new CookieOptions
            {
                Secure = true,
                HttpOnly = false,
                SameSite = SameSiteMode.None,
            });

            Response.Cookies.Append("Role", role, new CookieOptions
            {
                Secure = true,
                HttpOnly = false,
                SameSite = SameSiteMode.None,
            });
            UserLoginDTO user2 = new UserLoginDTO()
            {
                name = userdto.name,
                password = userdto.password,
                role = role,
                IsActive = GetUser(userdto.name).IsActive,
            };

            return Ok(user2);
        }

        [HttpGet("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<User>> GetAllUsers2()
        {
            List<User> items = _db.Users.ToList();
            return Ok(items);
        }
        private List<User> GetAllUsers()
        {
            List<User> items = _db.Users.ToList();
            return items;
        }

        [HttpGet("GetUser/{Username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UserDTO> GetUser2(string Username)
        {
            if (Username == null || Username == "")
            {
                return BadRequest("Invalid Username");
            }
            User? User = GetUser(Username);
            if (User == null)
            {
                return NotFound("User not found.");
            }
            User.cars = _db.Users.Include(u => u.cars).FirstOrDefault(c => c.UserName == Username).cars;
            UserDTO user = new UserDTO()
            {
                username = User.UserName,
                isActive = User.IsActive,
                //cars = _db.Users.Include(u => u.cars) .FirstOrDefault(u => u.UserName == Username).cars.ToList(),
                //cars = GetRecommendedCarsForUser(Username)
                cars = []
            };
            

            return Ok(user);
        }
        //public async Task<List<Car>> GetRecommendedCarsForUser(int userId)
        //{
        //    using (var context = new DatabaseContext(options))
        //    {
        //        User user = await _db.Users
        //            .Include(u => u.cars)
        //            .FirstOrDefaultAsync(u => u.Id == userId);

        //        if (user != null)
        //        {
        //            return user.cars.ToList();
        //        }
        //        else
        //        {
        //            return new List<Car>();
        //        }
        //    }
        //}
        //[HttpGet("recommended-cars/{userId}")]
        //public async Task<IActionResult> GetRecommendedCarsForUser3(int userId)
        //{
        //    try
        //    {
        //        var cars = await GetRecommendedCarsForUser(userId);
        //        return Ok(cars);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it appropriately
        //        return StatusCode(500, "An error occurred while retrieving the recommended cars.");
        //    }
        //}

        //public List<Car> GetRecommendedCarsForUser2(int userId)
        //{
        //    var user = _db.Users.Include(u=>u.cars).FirstOrDefault(u=>u.Id == userId);
        //    if(user != null)
        //    {
        //        return user.cars.ToList();
        //    }else
        //    {
        //        return new List<Car>();
        //    }
        //}


        private User? GetUser(string Username)
        {
            if (Username == null || Username == "")
            {
                return null;
            }
            User? User = _db.Users.FirstOrDefault(u => u.UserName == Username);
            return User;
        }

        [HttpPost("AddUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public ActionResult Add(AddUserDTO userdto)
        {
            //if (!IsAdmin())
            //{
            //    return Unauthorized("unauthorized access");
            //}
            if (string.IsNullOrEmpty(userdto.UserName) || string.IsNullOrEmpty(userdto.Password))
            {
                return BadRequest("User Can't Be Empty.");
            }
            if(GetUser(userdto.UserName) == null)
            {
                User user = new User(userdto.UserName, userdto.Password, false);

                _db.Users.Add(user);
                _db.SaveChanges();
                return Ok($"{userdto.UserName} Has Been Added Successfully");
            }
            return Conflict($"{userdto.UserName} Already Exist");
        }
        //public ActionResult<string> AddUser([FromBody] string name)
        //{
        //    //if (!IsAdmin())
        //    //{
        //    //    return Unauthorized("Unauthorized Access");
        //    //}
        //    if(name == null || name == "")
        //    {
        //        return BadRequest("User Can't Be Empty.");
        //    }
        //    if(GetUser(name) == null)
        //    {
        //        int userid = GetAllUsers().Count;
        //        User u = new User(name , )
        //        {
        //            Id = userid,
        //            UserName = name,
        //            cars = []

        //        };
                

        //        string json = System.IO.File.ReadAllText("Json/users.json");
        //        List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
        //        users.Add(u);
        //        string updatedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
        //        System.IO.File.WriteAllText("Json/users.json", updatedJson);

        //        return Ok($"{u.UserName} Has Been Added Successfully");
        //    }
        //    return Conflict($"{name} Already Exist");
        //}

        [HttpGet("GetAllCars")]
        private ActionResult<List<Car>> GetAllCars2()
        {

            
            List<Car> items = _db.Cars.ToList();
            return Ok(items);
        }

        private List<Car> GetAllCars()
        {

            List<Car> items = _db.Cars.ToList();
            return items;
        }

        [HttpGet("GetCarById/{id}")]

        public ActionResult<Car>? GetCar2(int id)
        {
            if (id == null || id < 0)
            {
                return null;
            }
            Car? car = _db.Cars.FirstOrDefault(u => u.Id == id);
            return Ok(car);
        }
        private Car? GetCar(int id)
        {
            if (id == null || id < 0)
            {
                return null;
            }
            Car? car = _db.Cars.FirstOrDefault(u => u.Id == id);
            return car;
        }

        [HttpGet("GetCarByName/{name}")]
        private Car? GetCarByName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return null;
            }
            Car? car = _db.Cars.FirstOrDefault(u => u.Name == name);
            return car;
        }


        [HttpPost("AddCar")]
        public ActionResult<string> AddCar(AddCarDTO car)
        {
            if (String.IsNullOrEmpty(car.name))
            {
                return BadRequest("Name Can't Be Empty.");
            }
            if (car.model == null || car.model > DateTime.Now.Year)
            {
                return BadRequest("Invalid Car model.");
            }

            if (GetCarByName(car.name) == null)
            {
                Car c = new Car(car.name, car.model);
                _db.Cars.Add(c);
                _db.SaveChanges();
                return Ok($" {c.Name} Has Been Added Successfully");
            }
            return Conflict($"{car.name} Already Exist");

        }

        [HttpPut("recommend")]
        public ActionResult<string> Recommed(recommendDTO rdto)
        {
            if (rdto.carid == null || rdto.carid < 0)
            {
                return null;
            }

            User user = GetUser(rdto.username);
            Car car = _db.Cars.FirstOrDefault(c => c.Id == rdto.carid);
            if (user != null)
            {
                foreach( Car c in user.cars) {
                    if(c.Id == rdto.carid)
                    {
                        return Conflict($"Car Already Recommended To {user.UserName}");
                    }
                }
                user.cars.Add(car);
                _db.Users.Update(user);
                _db.SaveChanges();
                return Ok($" {car.Name} Has Been Added Successfully to {user.UserName}");
                
            }

            return NotFound($"Not Found This User");
        }

        private bool IsAdmin()
        {
            var username = Request.Cookies["Username"];
            var role = Request.Cookies["Role"];

            return username == "amin" && role == "Admin";
        }


        [HttpPut("Password")]
        public ActionResult<string> Password(passwordDTO uu)
        {
            User u = GetUser(uu.username);
            if (u == null)
            {
                return NotFound("NOt Found");
            }
            if (!u.VerifyPassword(uu.OldPassword))
            {
                return BadRequest("Not Matched With Old Password");
            }else
            {
                u.UpdatePassword(uu.newPassword);
                u.IsActive = true;
                _db.Update(u);
                _db.SaveChanges();
                return Ok("Password Updated Successfully");
            }
        }

    }  
}
