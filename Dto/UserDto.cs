namespace FoodOrderingApi.Dto
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? OrderId { get; set; }
        public int? ReviewId { get; set; }
        public int? CartId { get; set; }
        public int? PaymentId { get; set; }



    }
}
