using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingApi.Model
{
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
