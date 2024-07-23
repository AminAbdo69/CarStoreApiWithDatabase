document
  .getElementById("form")
  .addEventListener("submit", async function (event) {
    event.preventDefault(); // Prevent form submission

    var oldpassword = document.getElementById("pass").value;
    var newPassword = document.getElementById("new1").value;
    var confirmed = document.getElementById("new2").value;

    var username = sessionStorage.getItem("name");
    var password = sessionStorage.getItem("password");
    var active = sessionStorage.getItem("active");

    if (oldpassword != password) {
      alert("Incorrect Old Password");
    }

    if (newPassword != confirmed) {
      alert("Invalid New Password or Confirmed");
    }
    const data = {
      username: username,
      OldPassword: oldpassword,
      newPassword: newPassword,
    };
    try {
      const response = await fetch(
        "https://localhost:7088/api/Admin/Password",
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(data),
        }
      );

      if (response.ok) {
        const result = await response.text();
        document.getElementById("output").innerText = result;
        oldpassword = "";
        newPassword = "";
        confirmed = "";
      } else {
        document.getElementById("Error").textContent = "Error adding car.";
      }
    } catch (error) {
      console.error("An error occurred:", error);
      document.getElementById("Error").textContent = "An error occurred.";
    }
  });
