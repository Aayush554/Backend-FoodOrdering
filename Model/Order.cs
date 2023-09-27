using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingApi.Model
{
    /*
     NAME

        Order - Represents an order.

     DESCRIPTION

        The Order class defines the structure of an order made by a user.
        It includes properties for the order's ID, order date, associated payment, user, ordered items, and total price.

     PROPERTIES

        - Id (int?): The unique identifier for the order.
        - OrderDate (DateTime?): The date and time when the order was placed.
        - PaymentId (int?): The ID of the associated payment for the order.
        - Payment (Payment?): The payment associated with the order.
        - UserId (int?): The ID of the user who placed the order.
        - User (User?): The user who placed the order.
        - OrderedItems (List<OrderedItems>?): Collection of ordered items included in the order.
        - TotalPrice (double): The total price of the order.

     FOREIGN KEYS

        - PaymentId (int?): Foreign key referencing the associated payment.
        - UserId (int?): Foreign key referencing the user who placed the order.

     */
    public class Order
    {
        public int? Id { get; set; }
        public DateTime? OrderDate { get; set; }

        [ForeignKey("Payment")]
        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User? User { get; set; }

        public List<OrderedItems>? OrderedItems { get; set; }
        public double TotalPrice { get; set; }
    }
}
