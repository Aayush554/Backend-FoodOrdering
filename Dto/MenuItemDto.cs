/*
 * NAME
 * 
 * MenuItemDto - Data Transfer Object (DTO) for representing a menu item.
 * 
 * DESCRIPTION
 * 
 * This class defines a DTO for representing a menu item. It includes properties to store information about the menu item,
 * such as its unique identifier, name, description, price, image URL, availability status, category ID, and associated reviews.
 */
using FoodOrderingApi.Model;

namespace FoodOrderingApi.Dto
{
    public class MenuItemDto
    {
        /*
         * PROPERTY
         * 
         * Id - Gets or sets the unique identifier of the menu item.
         */
        public int? Id { get; set; }

        /*
         * PROPERTY
         * 
         * Name - Gets or sets the name of the menu item.
         */
        public string? Name { get; set; }

        /*
         * PROPERTY
         * 
         * Description - Gets or sets the description of the menu item.
         */
        public string? Description { get; set; }

        /*
         * PROPERTY
         * 
         * Price - Gets or sets the price of the menu item.
         */
        public decimal? Price { get; set; }

        /*
         * PROPERTY
         * 
         * ImageUrl - Gets or sets the URL of the image associated with the menu item.
         */
        public string? ImageUrl { get; set; }

        /*
         * PROPERTY
         * 
         * IsAvailable - Gets or sets the availability status of the menu item.
         */
        public bool? IsAvailable { get; set; }

        /*
         * PROPERTY
         * 
         * CategoryId - Gets or sets the unique identifier of the category to which the menu item belongs.
         */
        public int? CategoryId { get; set; }

        /*
         * PROPERTY
         * 
         * Reviews - Gets or sets a collection of reviews associated with the menu item.
         */
        public ICollection<Review>? Reviews { get; set; }
    }
}
