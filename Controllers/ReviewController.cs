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
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }
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
