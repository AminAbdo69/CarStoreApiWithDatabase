using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace CarStoreApi
{
    public class User
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];

        public bool IsActive { get; set; } = false;
        public bool IsAdmin { get; set; } = false;


        public List<Car> cars { get; set; } = [];
        public User( string UserName, string Password, bool IsAdmin)
        {
            this.UserName = UserName;
            this.IsAdmin = IsAdmin;

            CreatePasswordHash(Password, out byte[] PasswordHash, out byte[] PasswordSalt);

            this.PasswordHash = PasswordHash;
            this.PasswordSalt = PasswordSalt;
        }
        public User(int Id , string UserName, string Password , bool IsAdmin)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.IsAdmin = IsAdmin;
          
            CreatePasswordHash(Password, out byte[] PasswordHash, out byte[] PasswordSalt);

            this.PasswordHash = PasswordHash;
            this.PasswordSalt = PasswordSalt;
        }

        public User(int Id, string UserName, byte[] PasswordHash, byte[] PasswordSalt , bool IsActive , bool IsAdmin)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.PasswordHash= PasswordHash;
            this.PasswordSalt = PasswordSalt;
            this.IsActive = IsActive;
            this.IsAdmin = IsAdmin;
        }



        private static void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using var hmac = new HMACSHA512();
            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
        }

        public bool VerifyPassword(string Password)
        {
            using var hmac = new HMACSHA512(PasswordSalt);
            byte[] ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
            return ComputedHash.SequenceEqual(PasswordHash);
        }

        public void UpdatePassword(string Password)
        {
            CreatePasswordHash(Password, out byte[] PasswordHash, out byte[] PasswordSalt);

            this.PasswordHash = PasswordHash;
            this.PasswordSalt = PasswordSalt;
        }
        
       
    }
}
