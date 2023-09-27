using AutoMapper;
using FoodOrderingApi.Dto;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using FoodOrderingApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository, IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
        }

        /*
         NAME

            GetReviews - Retrieve a list of reviews.

         DESCRIPTION

            This method retrieves a list of reviews and returns a 200 OK response if successful.
            It returns a 400 Bad Request response if there is an issue with the ModelState.

         RETURNS

            Returns a 200 OK response with a list of reviews if successful.
            Returns a 400 Bad Request response if there is an issue with the ModelState.
         */
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        /*
         NAME

            GetReviewByMenuItem - Retrieve a review for a specific menu item.

         DESCRIPTION

            This method retrieves a review for a specific menu item by its ID and returns a 200 OK response if found.
            It returns a 400 Bad Request response if there is an issue with the ModelState or a 404 Not Found response if the review does not exist.

         PARAMETERS

            menuItemId - An integer representing the menu item's ID.

         RETURNS

            Returns a 200 OK response with the review (decimal) if found.
            Returns a 400 Bad Request response if there is an issue with the ModelState.
            Returns a 404 Not Found response if the review does not exist.
         */
        [HttpGet("{menuItemId}/review")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewByMenuItem(int menuItemId)
        {
            if (_reviewRepository.GetReviewByMenuItemId(menuItemId) == null)
                return NotFound();

            var reviews = _reviewRepository.ReviewExists(menuItemId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        /*
         NAME

            DeleteReview - Delete a review by its ID.

         DESCRIPTION

            This method deletes a review by its ID and returns a 204 No Content response if successful.
            It returns a 400 Bad Request response if there is an issue with the ModelState or a 404 Not Found response if the review does not exist.

         PARAMETERS

            reviewId - An integer representing the review's ID.

         RETURNS

            Returns a 204 No Content response if the review is successfully deleted.
            Returns a 400 Bad Request response if there is an issue with the ModelState.
            Returns a 404 Not Found response if the review does not exist.
         */
        [HttpDelete("{reviewId}/deleteReview")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReview(reviewsToDelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            return NoContent();
        }

        /*
         NAME

            DeleteALLReviewS - Delete all reviews for a user.

         DESCRIPTION

            This method deletes all reviews for a user by their ID and returns a 204 No Content response if successful.
            It returns a 400 Bad Request response if there is an issue with the ModelState or a 404 Not Found response if the user does not exist.

         PARAMETERS

            userId - An integer representing the user's ID.

         RETURNS

            Returns a 204 No Content response if all reviews for the user are successfully deleted.
            Returns a 400 Bad Request response if there is an issue with the ModelState.
            Returns a 404 Not Found response if the user does not exist.
         */
        [HttpDelete("{userId}/deleteReviews")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteALLReviewS(int userId)
        {
            if ((bool)!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetReviewByUserId(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var reviewMap = _mapper.Map<Review>(reviewsToDelete);
            if (!_reviewRepository.DeleteReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            return NoContent();
        }

        /*
         NAME

            UpdateReview - Update a review.

         DESCRIPTION

            This method updates a review based on the provided ReviewDto object and user and menu item IDs.
            It returns a 204 No Content response if successful, or returns appropriate error responses for validation or server errors.

         PARAMETERS

            menuItemId - An integer representing the menu item's ID.
            userId - An integer representing the user's ID.
            updatedReview - A ReviewDto object representing the updated review.

         RETURNS

            Returns a 204 No Content response if the review is successfully updated.
            Returns a 400 Bad Request response for validation errors.
            Returns a 404 Not Found response if the review does not exist.
            Returns a 500 Internal Server Error response for server errors.
         */
        [HttpPut("{userId}/{menuItemId}/updateReviews")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview([FromQuery] int menuItemId,
                [FromQuery] int userId,
                [FromBody] ReviewDto updatedReview)
        {
            if (updatedReview == null)
                return BadRequest(ModelState);

            if (userId != updatedReview.Id)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists((int)updatedReview.Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var revkewMap = _mapper.Map<Review>(updatedReview);

            if (!_reviewRepository.UpdateReview(revkewMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
