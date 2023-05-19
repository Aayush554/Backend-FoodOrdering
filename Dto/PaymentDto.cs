namespace FoodOrderingApi.Dto
{
    public class PaymentDto
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int OrderId { get; set; }
    }
}
