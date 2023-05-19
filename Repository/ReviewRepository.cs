using AutoMapper;
using FoodOrderingApi.Data;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;

namespace FoodOrderingApi.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ReviewRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CreateReview(Review review)
        {
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(int reviewId)
        {
           return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviewByMenuItemId(int menuItemId)
        {
            return _context.Reviews.Where(r => r.MenuItemId == menuItemId).ToList();
        }

        public ICollection<Review> GetReviewByMenuItemName(string menuItemName)
        {
            return _context.Reviews.Where(r => r.MenuItem.Name == menuItemName).ToList();

        }

        public ICollection<Review> GetReviewByUserId(int userId)
        {
            return _context.Reviews.Where(r => r.UserId == userId).ToList();

        }

        public ICollection<Review> GetReviewByUserName(string userName)
        {
            return _context.Reviews.Where(r => r.User.Name == userName).ToList();

        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }
    }
}
