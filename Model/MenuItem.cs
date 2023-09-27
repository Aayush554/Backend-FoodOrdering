using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingApi.Model
{
    /*
     NAME

        MenuItem - Represents a menu item.

     DESCRIPTION

        The MenuItem class defines the structure of a menu item.
        It includes properties for the item's ID, name, description, price, image URL, availability, category, reviews, cart items, and ordered items.

     PROPERTIES

        - Id (int?): The unique identifier for the menu item.
        - Name (string?): The name of the menu item.
        - Description (string?): A brief description of the menu item.
        - Price (decimal?): The price of the menu item.
        - ImageUrl (string?): The URL of the menu item's image.
        - IsAvailable (bool?): Indicates whether the menu item is currently available.
        - CategoryId (int?): The ID of the category to which the menu item belongs.
        - Category (Category?): The category to which the menu item belongs.
        - Reviews (List<Review>?): Collection of reviews related to the menu item.
        - CartItems (List<CartItem>?): Collection of cart items where this menu item is added.
        - OrderedItems (List<OrderedItems>?): Collection of ordered items related to this menu item.

     FOREIGN KEYS

        - CategoryId (int?): Foreign key referencing the associated category.

     */
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

        public List<Review>? Reviews { get; set; }
        public List<CartItem>? CartItems { get; set; }
        public List<OrderedItems>? OrderedItems { get; set; }
    }
}
