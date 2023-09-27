using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingApi.Model
{
    /*
     NAME

        OrderedItems - Represents an item within an order.

     DESCRIPTION

        The OrderedItems class defines the structure of an item within an order.
        It includes properties for the order ID, associated order, menu item ID, associated menu item, quantity, and price.

     PROPERTIES

        - OrderId (int?): The ID of the order to which the item belongs.
        - Order (Order?): The order to which the item belongs.
        - MenuItemId (int?): The ID of the associated menu item.
        - MenuItem (MenuItem?): The menu item associated with the ordered item.
        - Quantity (int?): The quantity of the menu item ordered.
        - Price (decimal?): The price of the menu item at the time of ordering.

     FOREIGN KEYS

        - OrderId (int?): Foreign key referencing the associated order.
        - MenuItemId (int?): Foreign key referencing the associated menu item.

     */
    public class OrderedItems
    {
        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public Order? Order { get; set; }

        [ForeignKey("MenuItem")]
        public int? MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }

        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}
