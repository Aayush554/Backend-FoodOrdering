
using AutoMapper;
using FoodOrderingApi.Dto;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FoodOrderingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private ICartRepository _cartRepository;
        private IMapper _mapper;

        public UserController(IUserRepository userRepository, ICartRepository cartRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }
        [HttpGet("count")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        public IActionResult GetUserCounttUser()
        {
            var count = _userRepository.GetUserCount();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(count);

        }
        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult GetUser(int userId)
        {
            if((bool)!_userRepository.UserExists(userId))
                return NotFound();  
            var user = _mapper.Map<UserDto>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);

        }
        [HttpGet("{userName}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult GetUserByName(string userName)
        {
            var user = _mapper.Map<UserDto>(_userRepository.GetUserByName(userName));
            if (user == null)   
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);

        }
        [HttpGet("{userId}/cartId")]
        [ProducesResponseType(200, Type = typeof(int))]
        public IActionResult GetCartIdByUserId(int userId)
        {
            if ((bool)!_userRepository.UserExists(userId))
                return NotFound();
            int cartId = (int)_userRepository.GetCartIdByUserId(userId);
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cartId);

        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromQuery] CartDto cartCreate, [FromBody] UserDto userCreate)
        {
            if(userCreate == null || cartCreate == null) return BadRequest(ModelState);
            var user = _userRepository.GetUsers()
                .Where(c => c.UserName.Trim().ToUpper() == userCreate.UserName.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (user != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userMap = _mapper.Map<User>(userCreate);
            var cartMap = _mapper.Map<Cart>(cartCreate);
            userMap.CartId = cartMap.Id;
            userMap.Cart = cartMap;
            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            if (!_cartRepository.CreateCart(cartMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");

        }
        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (userId != updatedUser.Id)
                return BadRequest(ModelState);

            if ((bool)!_userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var userMap = _mapper.Map<User>(updatedUser);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int userId)
        {

            if ((bool)!_userRepository.UserExists(userId))
                return NotFound();

            var userToDelete = _userRepository.GetUser(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }

            return NoContent();
        }
    }
}
