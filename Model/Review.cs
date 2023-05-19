using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingApi.Model
{
    public class Review
    {
        public int? Id { get; set; }
        public int? Rating { get; set; }
        public string? ReviewMessage { get; set; }
        public DateTime ReviewDate { get; set; }

        [ForeignKey("MenuItem")]
        public int? MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
        
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
