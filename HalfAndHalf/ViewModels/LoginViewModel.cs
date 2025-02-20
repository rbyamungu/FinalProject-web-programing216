using System.ComponentModel.DataAnnotations;

namespace HalfAndHalf.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;  // Changed from UserName to Email

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}