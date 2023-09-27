/*
 * NAME
 * 
 * CategoryDto - Data Transfer Object (DTO) for representing a category.
 * 
 * DESCRIPTION
 * 
 * This class defines a DTO for representing a category. It includes properties to store information about the category,
 * such as its unique identifier, name, and description.
 */
namespace FoodOrderingApi.Dto
{
    public class CategoryDto
    {
        /*
         * PROPERTY
         * 
         * Id - Gets or sets the unique identifier of the category.
         */
        public int? Id { get; set; }

        /*
         * PROPERTY
         * 
         * Name - Gets or sets the name of the category.
         */
        public string? Name { get; set; }

        /*
         * PROPERTY
         * 
         * Description - Gets or sets the description of the category.
         */
        public string? Description { get; set; }
    }
}
