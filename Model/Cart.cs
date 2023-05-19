using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingApi.Model
{
    public class Cart
    {
        public int? Id { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }

    }
}
