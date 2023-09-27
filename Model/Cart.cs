using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingApi.Model
{
    /*
     NAME

        Cart - Represents a shopping cart entity.

     DESCRIPTION

        The Cart class defines the structure of a shopping cart in the system. It includes properties
        for the cart's unique identifier, associated user, and a collection of cart items.

     PROPERTIES

        - Id (int?): The unique identifier for the cart.
        - UserId (int?): The ID of the user associated with the cart.
        - User (User?): The user associated with the cart.
        - CartItems (List<CartItem>): A collection of cart items associated with the cart.

     CONSTRUCTOR

        - Cart(): Initializes a new instance of the Cart class with an empty collection of cart items.

     RELATIONSHIPS

        - The "User" property is a foreign key referencing the associated user.
        - The "CartItems" property represents a navigation property to access the cart's items.

     */
    public class Cart
    {
        public int? Id { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User? User { get; set; }

        public List<CartItem> CartItems { get; set; } // Navigation property

        // Constructor to initialize the collection
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}
