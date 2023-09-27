using FoodOrderingApi.Model;

namespace FoodOrderingApi.Interfaces
{
    public interface IReviewRepository
    {
        /*
         NAME

            GetReviews - Retrieve all reviews.

         SYNOPSIS

            ICollection<Review> GetReviews()

         DESCRIPTION

            This function retrieves all reviews available.

         RETURNS

            Returns a collection of all reviews.
        */
        ICollection<Review> GetReviews();

        /*
         NAME

            GetReview - Retrieve a review by ID.

         SYNOPSIS

            Review GetReview(int reviewId)

         DESCRIPTION

            This function retrieves a review by its unique identifier.

         PARAMETERS

            reviewId - The unique identifier of the review.

         RETURNS

            Returns the review associated with the provided ID.
        */
        Review GetReview(int reviewId);

        /*
         NAME

            GetReviewByUserId - Retrieve reviews by user ID.

         SYNOPSIS

            ICollection<Review> GetReviewByUserId(int userId)

         DESCRIPTION

            This function retrieves reviews by the user's ID.

         PARAMETERS

            userId - The ID of the user associated with the reviews.

         RETURNS

            Returns a collection of reviews associated with the provided user ID.
        */
        ICollection<Review> GetReviewByUserId(int userId);

        /*
         NAME

            GetReviewByUserName - Retrieve reviews by user name.

         SYNOPSIS

            ICollection<Review> GetReviewByUserName(string userName)

         DESCRIPTION

            This function retrieves reviews by the user's name.

         PARAMETERS

            userName - The name of the user associated with the reviews.

         RETURNS

            Returns a collection of reviews associated with the provided user name.
        */
        ICollection<Review> GetReviewByUserName(string userName);

        /*
         NAME

            GetReviewByMenuItemId - Retrieve reviews by menu item ID.

         SYNOPSIS

            ICollection<Review> GetReviewByMenuItemId(int menuItemId)

         DESCRIPTION

            This function retrieves reviews by the menu item's ID.

         PARAMETERS

            menuItemId - The ID of the menu item associated with the reviews.

         RETURNS

            Returns a collection of reviews associated with the provided menu item ID.
        */
        ICollection<Review> GetReviewByMenuItemId(int menuItemId);

        /*
         NAME

            GetReviewByMenuItemName - Retrieve reviews by menu item name.

         SYNOPSIS

            ICollection<Review> GetReviewByMenuItemName(string menuItemName)

         DESCRIPTION

            This function retrieves reviews by the menu item's name.

         PARAMETERS

            menuItemName - The name of the menu item associated with the reviews.

         RETURNS

            Returns a collection of reviews associated with the provided menu item name.
        */
        ICollection<Review> GetReviewByMenuItemName(string menuItemName);

        /*
         NAME

            ReviewExists - Check if a review exists.

         SYNOPSIS

            bool ReviewExists(int reviewId)

         DESCRIPTION

            This function checks if a review exists based on its unique identifier.

         PARAMETERS

            reviewId - The unique identifier of the review.

         RETURNS

            Returns true if the review exists; otherwise, false.
        */
        bool ReviewExists(int reviewId);

        /*
         NAME

            CreateReview - Create a new review.

         SYNOPSIS

            bool CreateReview(Review review)

         DESCRIPTION

            This function creates a new review.

         PARAMETERS

            review - The review to be created.

         RETURNS

            Returns true if the review was successfully created; otherwise, false.
        */
        bool CreateReview(Review review);

        /*
         NAME

            UpdateReview - Update an existing review.

         SYNOPSIS

            bool UpdateReview(Review review)

         DESCRIPTION

            This function updates an existing review.

         PARAMETERS

            review - The review to be updated.

         RETURNS

            Returns true if the review was successfully updated; otherwise, false.
        */
        bool UpdateReview(Review review);

        /*
         NAME

            DeleteReview - Delete a review.

         SYNOPSIS

            bool DeleteReview(Review review)

         DESCRIPTION

            This function deletes a review.

         PARAMETERS

            review - The review to be deleted.

         RETURNS

            Returns true if the review was successfully deleted; otherwise, false.
        */
        bool DeleteReview(Review review);

        /*
         NAME

            DeleteReviews - Delete multiple reviews.

         SYNOPSIS

            bool DeleteReviews(List<Review> reviews)

         DESCRIPTION

            This function deletes multiple reviews.

         PARAMETERS

            reviews - The list of reviews to be deleted.

         RETURNS

            Returns true if the reviews were successfully deleted; otherwise, false.
        */
        bool DeleteReviews(List<Review> reviews);

        /*
         NAME

            Save - Save changes to the data store.

         SYNOPSIS

            bool Save()

         DESCRIPTION

            This function saves any changes made to the data store.

         RETURNS

            Returns true if changes were successfully saved; otherwise, false.
        */
        bool Save();
    }
}
