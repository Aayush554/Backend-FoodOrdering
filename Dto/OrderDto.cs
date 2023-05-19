namespace FoodOrderingApi.Dto
{
    public class OrderDto
    {
        public int? Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? PaymentId { get; set; }
        public int? UserId { get; set; }
        public int? CartId { get; set; }
    }
}
