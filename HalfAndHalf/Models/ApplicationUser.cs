namespace HalfAndHalf.Models
{

    public class ApplicationUser : IdentityUser
    {
        public string? ProfilePhotoUrl { get; set; }
        public int Id { get; set; }  // Primary key
        public string Username { get; set; }  // Stored as plain text
        public string PasswordHash { get; set; }  // Hashed password
        public string Salt { get; set; }  // Salt for hashing
    }
}
