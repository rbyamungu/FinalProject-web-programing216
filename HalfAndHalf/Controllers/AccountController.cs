using Microsoft.AspNetCore.Mvc;
using HalfAndHalf.Models; // Make sure this matches your namespace
using HalfAndHalf.Helpers;
using HalfAndHalf.Data;
using System.Linq;

namespace HalfAndHalf.Controllers // Make sure this matches your project name
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }


         [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Register(string username, string password)
        {
            string salt, hashedPassword;
            hashedPassword = PasswordHelper.HashPassword(password, out salt);

            var user = new User
            {
                Username = username,
                PasswordHash = hashedPassword,
                Salt = salt
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}
