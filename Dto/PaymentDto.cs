/*
 * NAME
 * 
 * PaymentDto - Data Transfer Object (DTO) for representing a payment.
 * 
 * DESCRIPTION
 * 
 * The PaymentDto class represents a Data Transfer Object (DTO) for payments in the Food Ordering API. DTOs are used to transfer data between different parts of an application, typically between the client and server. This class contains properties that mirror the structure of a payment entity, allowing for efficient data transfer.
 */
namespace FoodOrderingApi.Dto
{
    public class PaymentDto
    {
        /*
         * PROPERTY
         * 
         * Id (int?): Gets or sets the unique identifier of the payment.
         */
        public int? Id { get; set; }

        /*
         * PROPERTY
         * 
         * UserId (int?): Gets or sets the identifier of the user associated with the payment.
         */
        public int? UserId { get; set; }

        /*
         * PROPERTY
         * 
         * OrderId (int): Gets or sets the identifier of the order associated with the payment.
         */
        public int OrderId { get; set; }
    }
}
