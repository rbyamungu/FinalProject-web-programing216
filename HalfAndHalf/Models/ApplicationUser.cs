using Microsoft.AspNetCore.Identity;

namespace HalfAndHalf.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? ProfilePhotoUrl { get; set; }
    }
}