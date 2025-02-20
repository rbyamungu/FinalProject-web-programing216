using System.ComponentModel.DataAnnotations;

namespace HalfAndHalf.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            Email = string.Empty;
            Password = string.Empty;
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}