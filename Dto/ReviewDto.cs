namespace FoodOrderingApi.Dto
{
    public class ReviewDto
    {
        public int? Id { get; set; }
        public int? Rating { get; set; }
        public string? ReviewMessage { get; set; }
        public DateTime ReviewDate { get; set; }
        public int? MenuItemId { get; set; }
        public int? UserId { get; set; }

    }
}
