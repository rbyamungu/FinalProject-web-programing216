using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HalfAndHalf.Models;
using HalfAndHalf.Helpers;
using HalfAndHalf.Data;
using HalfAndHalf.ViewModels;
using HalfAndHalf.Services;
using System.Linq;

namespace HalfAndHalf.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IStorageService _storageService;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IStorageService storageService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _storageService = storageService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                
                if (model.ProfilePhoto != null)
                {
                    // Convert IFormFile to byte array
                    using var memoryStream = new MemoryStream();
                    await model.ProfilePhoto.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    // Generate a unique filename
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ProfilePhoto.FileName)}";

                    // Upload the file
                    var uploadedFileName = await _storageService.UploadFileAsync(fileName, fileBytes);
                    user.ProfilePhotoUrl = uploadedFileName;
                }

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
