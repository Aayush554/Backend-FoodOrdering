using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingApi.Model
{
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

        [ForeignKey("Cart")]
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }

        public double TotalPrice { get; set; }


    }

}
