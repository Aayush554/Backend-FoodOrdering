using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingApi.Model
{
    public class MenuItem
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsAvailable { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
