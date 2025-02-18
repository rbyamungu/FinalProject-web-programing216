using Microsoft.AspNetCore.Identity;

namespace HalfAndHalf.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}