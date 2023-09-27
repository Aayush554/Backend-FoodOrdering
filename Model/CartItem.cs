using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApi.Model
{
    /*
     NAME

        CartItem - Represents an item within a shopping cart.

     DESCRIPTION

        The CartItem class defines the structure of an item within a shopping cart in the system.
        It includes properties for the cart item's unique identifier, associated cart, associated menu item,
        quantity, and price.

     PROPERTIES

        - CartId (int?): The unique identifier for the cart to which this item belongs.
        - Cart (Cart?): The cart to which this item belongs (navigation property).
        - MenuItemId (int?): The unique identifier for the menu item associated with this cart item.
        - MenuItem (MenuItem?): The menu item associated with this cart item (navigation property).
        - Quantity (int?): The quantity of this menu item in the cart.
        - Price (decimal?): The price of this cart item.

     KEY ATTRIBUTES

        - The "CartId" property is marked with the [Key] attribute as it's part of the composite primary key
          along with "MenuItemId".
        - The "MenuItemId" property is marked with the [Key] attribute as it's part of the composite primary key
          along with "CartId".

     RELATIONSHIPS

        - The "Cart" property represents a navigation property to access the cart to which this item belongs.
        - The "MenuItem" property represents a navigation property to access the associated menu item.

     */
    public class CartItem
    {
        [Key]
        [ForeignKey("Cart")]
        public int? CartId { get; set; }

        public Cart? Cart { get; set; }

        [Key]
        [ForeignKey("MenuItem")]
        public int? MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }
    }
}
