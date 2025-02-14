using Microsoft.AspNetCore.Mvc;
using HalfAndHalf.Models;
using HalfAndHalf.Helpers;
using HalfAndHalf.Data;
using System.Linq;

namespace HalfAndHalf.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || !PasswordHelper.VerifyPassword(password, user.Salt, user.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
