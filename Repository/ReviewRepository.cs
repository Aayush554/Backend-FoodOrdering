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

                /*
         NAME

            CreateReview - Creates a new review.

         DESCRIPTION

            This method is used to create a new review and save it to the data context.

         PARAMETERS

            review - The Review object representing the review to be created.

         RETURNS

            Returns true if the review creation was successful; otherwise, false.
         */
        public bool CreateReview(Review review)
        {
            return Save();
        }

                /*
         NAME

            DeleteReview - Deletes a review.

         DESCRIPTION

            This method removes a review from the data context.

         PARAMETERS

            review - The Review object representing the review to be deleted.

         RETURNS

            Returns true if the review deletion was successful; otherwise, false.
         */
        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

                /*
         NAME

            DeleteReviews - Deletes a list of reviews.

         DESCRIPTION

            This method removes a list of reviews from the data context.

         PARAMETERS

            reviews - A list of Review objects representing the reviews to be deleted.

         RETURNS

            Returns true if the reviews deletion was successful; otherwise, false.
         */
        public bool DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }

                /*
         NAME

            GetReview - Retrieves a review by review ID.

         DESCRIPTION

            This method retrieves a Review object with the specified review ID from the data context.

         PARAMETERS

            reviewId - An integer representing the ID of the review.

         RETURNS

            Returns the Review object representing the review.
         */
        public Review GetReview(int reviewId)
        {
           return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }


        /*
         NAME

            GetReviewByMenuItemId - Retrieves reviews by menu item ID.

         DESCRIPTION

            This method retrieves a collection of Review objects associated with the specified menu item ID from the data context.

         PARAMETERS

            menuItemId - An integer representing the ID of the menu item.

         RETURNS

            Returns a collection of Review objects representing the reviews for the menu item.
         */
        public ICollection<Review> GetReviewByMenuItemId(int menuItemId)
        {
            return _context.Reviews.Where(r => r.MenuItemId == menuItemId).ToList();
        }


        /*
         NAME

            GetReviewByMenuItemName - Retrieves reviews by menu item name.

         DESCRIPTION

            This method retrieves a collection of Review objects associated with the specified menu item name from the data context.

         PARAMETERS

            menuItemName - A string representing the name of the menu item.

         RETURNS

            Returns a collection of Review objects representing the reviews for the menu item.
         */
        public ICollection<Review> GetReviewByMenuItemName(string menuItemName)
        {
            return _context.Reviews.Where(r => r.MenuItem.Name == menuItemName).ToList();

        }
                /*
         NAME

            GetReviewByUserId - Retrieves reviews by user ID.

         DESCRIPTION

            This method retrieves a collection of Review objects associated with the specified user ID from the data context.

         PARAMETERS

            userId - An integer representing the ID of the user.

         RETURNS

            Returns a collection of Review objects representing the reviews created by the user.
         */
        
        public ICollection<Review> GetReviewByUserId(int userId)
        {
            return _context.Reviews.Where(r => r.UserId == userId).ToList();

        }

                /*
         NAME

            GetReviewByUserName - Retrieves reviews by user name.

         DESCRIPTION

            This method retrieves a collection of Review objects associated with the specified user name from the data context.

         PARAMETERS

            userName - A string representing the name of the user.

         RETURNS

            Returns a collection of Review objects representing the reviews created by the user.
         */
        public ICollection<Review> GetReviewByUserName(string userName)
        {
            return _context.Reviews.Where(r => r.User.Name == userName).ToList();

        }

                /*
         NAME

            GetReviews - Retrieves all reviews.

         DESCRIPTION

            This method retrieves all Review objects from the data context.

         RETURNS

            Returns a collection of Review objects representing all reviews.
         */
        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

                /*
         NAME

            ReviewExists - Checks if a review exists by review ID.

         DESCRIPTION

            This method checks if a review with the specified review ID exists in the data context.

         PARAMETERS

            reviewId - An integer representing the ID of the review.

         RETURNS

            Returns true if the review exists; otherwise, false.
         */
        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }

                /*
         NAME

            Save - Saves changes to the data context.

         DESCRIPTION

            This method saves changes made to the data context.

         RETURNS

            Returns true if the changes were successfully saved; otherwise, false.
         */

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

                /*
         NAME

            UpdateReview - Updates a review.

         DESCRIPTION

            This method updates a review in the data context.

         PARAMETERS

            review - The Review object representing the updated review.

         RETURNS

            Returns true if the review update was successful; otherwise, false.
         */
        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }
    }
}
