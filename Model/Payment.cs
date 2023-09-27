using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FoodOrderingApi.Model
{
    /*
     NAME

        Payment - Represents a payment made by a user for an order.

     DESCRIPTION

        The Payment class defines the structure of a payment made by a user for an order.
        It includes properties for the payment ID, card number, cardholder name, expiry date, CVV, user ID, associated user, and a list of associated orders.

     PROPERTIES

        - Id (int?): The unique identifier for the payment.
        - CardNumber (string?): The card number used for the payment.
        - CardHolderName (string?): The name of the cardholder.
        - ExpiryDate (DateTime): The expiry date of the payment card.
        - CVV (int?): The Card Verification Value (CVV) of the payment card.
        - UserId (int?): The ID of the user who made the payment.
        - User (User?): The user associated with the payment.
        - OrderId (int): The ID of the order related to the payment.
        - Orders (List<Order>?): A list of orders associated with this payment.

     FOREIGN KEYS

        - UserId (int?): Foreign key referencing the associated user.
        - OrderId (int): Foreign key referencing the associated order.

     */
    public class Payment
    {
        public int? Id { get; set; }
        public string? CardNumber { get; set; }
        public string? CardHolderName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? CVV { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
