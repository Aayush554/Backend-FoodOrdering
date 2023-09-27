
using FoodOrderingApi.Model;

namespace FoodOrderingApi.Dto
{
    public class CartDto
    {
        /*
         * PROPERTY
         * 
         * Id - Gets or sets the unique identifier of the shopping cart.
         */
        public int? Id { get; set; }

        /*
         * PROPERTY
         * 
         * UserId - Gets or sets the user ID associated with the shopping cart.
         */
        public int? UserId { get; set; }

        /*
         * PROPERTY
         * 
         * CartItems - Gets or sets a collection of cart items associated with the shopping cart.
         */
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
