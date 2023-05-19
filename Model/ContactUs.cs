using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApi.Model
{
    public class ContactUs
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? FullName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(100)]
        public string? Subject { get; set; }

        [Required]
        [StringLength(500)]
        public string? Message { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
