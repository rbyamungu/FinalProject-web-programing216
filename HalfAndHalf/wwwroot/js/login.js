const form = document.querySelector('form');

form.addEventListener('submit', (event) => {
  event.preventDefault();
  
  const username = document.getElementById('username').value;
  const password = document.getElementById('password').value;
  
  // Validate user input and sanitize it
  if (!username || !password) {
    alert('Please enter both username and password');
    return;
  }
  
  // Use a salted hash to store the password
  const salt = 'my_secret_salt';
  const hash = bcrypt.hash(password, salt);
  
  // Store the hashed password in the database
  // ...
  
  // Redirect to the main page after successful login
  window.location.href = '/';
});


const forgetPasswordLink = document.getElementById("forget-password-link");

forgetPasswordLink.addEventListener("click", function() {
  // Navigate to the "Forget Password" page here
  window.location.href = "/login/forget";
});

/// Forget password page


const emailInput = document.getElementById("email");
const passwordResetButton = document.getElementById("password-reset-button");

passwordResetButton.addEventListener("click", () => {
  // Reset password logic here
  alert("Password reset successful!");
});