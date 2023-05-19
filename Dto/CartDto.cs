using FoodOrderingApi.Model;

namespace FoodOrderingApi.Dto
{
    public class CartDto
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
