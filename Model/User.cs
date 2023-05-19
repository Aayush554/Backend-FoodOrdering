using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingApi.Model
{
    public class User
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string?   Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }

        // Navigation properties
        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public List<Order>? Orders { get; set; }

        [ForeignKey("Review")]
        public int? ReviewId { get; set; }
        public List<Review>? Reviews { get; set; }

        [ForeignKey("Cart")]
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }

        [ForeignKey("Payment")]
        public int? PaymentId { get; set; }
        public List<Payment>? Payments { get; set; }
    }

}
