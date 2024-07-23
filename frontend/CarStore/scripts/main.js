const joinForm = document.getElementById("form");
const eee = document.getElementById("Errors");

joinForm.addEventListener("submit", HandleSubmit);

async function HandleSubmit(e) {
  e.preventDefault();

  const name = joinForm.Username.value;
  const password = joinForm.password.value;
  const data = {
    name,
    password,
  };
  try {
    if (!name) {
      eee.innerText = "Please Enter Any Name.";
      return;
    }
    if (!password) {
      eee.innerText = "Please Enter Any PassWord.";
      return;
    }

    const response = await fetch("https://localhost:7088/api/Admin/Login", {
      method: "POST",
      body: JSON.stringify(data),
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const getCookie = (key) => {
      const cookieString = `; ${document.cookie}`;
      const cookieParts = cookieString.split(`; ${key}=`);
      if (cookieParts.length === 2) {
        return cookieParts.pop().split(";").shift();
      }
      return null; // Cookie not found
    };

    // Usage:
    const usernameValue = getCookie("Username");
    const roleValue = getCookie("Role");

    if (usernameValue !== null) {
      console.log(`Username: ${usernameValue}`);
    } else {
      console.log("Username cookie not found.");
    }

    if (roleValue !== null) {
      console.log(`Role: ${roleValue}`);
    } else {
      console.log("Role cookie not found.");
    }

    if (response.ok) {
      const result = await response.json();
      console.log("HEllo");
      console.log(result);
      if (result.role == "Admin" && password == result.password) {
        sessionStorage.setItem("name", result.name);
        sessionStorage.setItem("password", result.password);
        sessionStorage.setItem("active", result.isActive);
        location.href = "/admin.html";
      } else if (
        result.role == "User" &&
        password == result.password &&
        result.isActive == false
      ) {
        sessionStorage.setItem("name", result.name);
        sessionStorage.setItem("password", result.password);
        sessionStorage.setItem("active", result.isActive);
        location.href = "/changePassword.html";
      } else if (
        result.role == "User" &&
        password == result.password &&
        result.isActive == true
      ) {
        sessionStorage.setItem("name", result.name);
        sessionStorage.setItem("password", result.password);
        sessionStorage.setItem("active", result.isActive);
        location.href = "/user.html";
      }
    } else {
      console.error("Error:", response.statusText);
    }
  } catch (error) {
    console.error("Error fetching data:", error);
  }

  // try {
  //   const name = joinForm.Username.value;

  //   if (!name) {
  //     eee.innerText = "Please Enter Any Name.";
  //     return;
  //   }

  //   const response = await fetch("https://localhost:7088/api/Admin/Login", {
  //     method: "POST",
  //     headers: {
  //       "Content-Type": "application/json",
  //     },
  //     body: JSON.stringify(name),
  //   });

  //   const message = await response.json();

  //   console.log(message);
  //   // location.href = "/admin.html";
  // } catch (e) {
  //   console.log(e);
  // }
}
