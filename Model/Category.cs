namespace FoodOrderingApi.Model
{
    /*
     NAME

        Category - Represents a food category.

     DESCRIPTION

        The Category class defines the structure of a food category in the system.
        It includes properties for the category's unique identifier, name, description, and associated menu items.

     PROPERTIES

        - Id (int?): The unique identifier for the category.
        - Name (string?): The name of the category.
        - Description (string?): A description of the category.
        - MenuItems (List<MenuItem>?): A collection of menu items associated with this category.

     RELATIONSHIPS

        - The "MenuItems" property represents a collection of navigation properties to access the menu items
          that belong to this category.

     */
    public class Category
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<MenuItem>? MenuItems { get; set; }
    }
}
