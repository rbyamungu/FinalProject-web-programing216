using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HalfAndHalf.Data;
using HalfAndHalf.Models;
using HalfAndHalf.Helpers;
using Microsoft.Extensions.Logging;

namespace HalfAndHalf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        // Constructor to inject dependencies
        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Handles user registration
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

        // Default Home Page
        public IActionResult Index()
        {
            return View();
        }

        // Privacy Policy Page
        public IActionResult Privacy()
        {
            return View();
        }

        // Login Page
        public IActionResult loginScreen()
        {
            return View();
        }

        // Forgot Password Page
        public IActionResult forget()
        {
            return View();
        }

        // Error Page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
