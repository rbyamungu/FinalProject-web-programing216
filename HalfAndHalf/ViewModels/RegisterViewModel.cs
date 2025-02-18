using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HalfAndHalf.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Profile Photo")]
        public IFormFile? ProfilePhoto { get; set; }
    }
}