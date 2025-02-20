using Microsoft.AspNetCore.Identity;

namespace HalfAndHalf.Models
{

    public class ApplicationUser : IdentityUser
    {
        public string? Salt { get; set; }

        public string? ProfilePhotoUrl { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
