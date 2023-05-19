using FoodOrderingApi.Model;

namespace FoodOrderingApi.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();

        Review GetReview(int reviewId);
        ICollection<Review> GetReviewByUserId(int userId);
        ICollection<Review> GetReviewByUserName(string userName);

        ICollection<Review> GetReviewByMenuItemId(int menuItemId);
        ICollection<Review> GetReviewByMenuItemName(string menuItemName);
        bool ReviewExists(int reviewId);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
        bool Save();
    }
}
