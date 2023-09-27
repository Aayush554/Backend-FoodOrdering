/*
NAME

    OrderDto - Data Transfer Object (DTO) for Order

SYNOPSIS

    public class OrderDto
    {
        // Properties
        public int? Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? PaymentId { get; set; }
        public int? UserId { get; set; }
        public int? CartId { get; set; }
    }

DESCRIPTION

    The OrderDto class represents a Data Transfer Object (DTO) for orders in the Food Ordering API. DTOs are used to transfer data between different parts of an application, typically between the client and server. This class contains properties that mirror the structure of an order entity, allowing for efficient data transfer.

PROPERTIES

    - Id (int?): Gets or sets the unique identifier of the order.
    - OrderDate (DateTime?): Gets or sets the date and time when the order was placed.
    - PaymentId (int?): Gets or sets the identifier of the associated payment for the order.
    - UserId (int?): Gets or sets the identifier of the user who placed the order.
    - CartId (int?): Gets or sets the identifier of the shopping cart associated with the order.

USAGE

    This DTO class is typically used for transferring order-related data between the client and server in a structured format.

SEE ALSO

    - Order: The corresponding entity class representing orders in the database.
*/
public class OrderDto
{
    // Properties
    public int? Id { get; set; }
    public DateTime? OrderDate { get; set; }
    public int? PaymentId { get; set; }
    public int? UserId { get; set; }
    public int? CartId { get; set; }
}
