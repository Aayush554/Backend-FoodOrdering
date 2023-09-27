namespace FoodOrderingApi.Model
{
    /*
     NAME

        Product - Represents a product available in the system.

     DESCRIPTION

        The Product class defines the structure of a product available in the system.
        It includes properties for the product ID, name, description, and price.

     PROPERTIES

        - Id (int): The unique identifier for the product.
        - Name (string): The name of the product.
        - Description (string): A brief description of the product.
        - Price (decimal): The price of the product.

     */
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
