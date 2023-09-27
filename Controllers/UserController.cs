using AutoMapper;
using FoodOrderingApi.Dto;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        /*
         NAME

            GetUsers - Get a list of users.

         DESCRIPTION

            This method retrieves a list of users and maps them to UserDto objects. It returns a 200 OK response
            with the list of users if successful.

         RETURNS

            Returns a 200 OK response with a list of users (IEnumerable<UserDto>) if successful.
            Returns a 400 Bad Request response in case of an exception.
         */
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        /*
         NAME

            GetUserCounttUser - Get the total count of users.

         DESCRIPTION

            This method retrieves the total count of users and returns it as a decimal value.
            It returns a 200 OK response with the user count if successful.

         RETURNS

            Returns a 200 OK response with the user count (decimal) if successful.
            Returns a 400 Bad Request response in case of an exception.
         */
        [HttpGet("count")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        public IActionResult GetUserCounttUser()
        {
            var count = _userRepository.GetUserCount();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(count);

        }

        /*
         NAME

            GetUser - Get a user by ID.

         DESCRIPTION

            This method retrieves a user by the specified user ID and maps it to a UserDto object.
            It returns a 200 OK response with the user if found, or a 404 Not Found response if the user does not exist.

         PARAMETERS

            userId - An integer representing the user's ID.

         RETURNS

            Returns a 200 OK response with the user (UserDto) if found.
            Returns a 404 Not Found response if the user does not exist.
         */
        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult GetUser(int userId)
        {
            if ((bool)!_userRepository.UserExists(userId))
                return NotFound();
            var user = _mapper.Map<UserDto>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);

        }

        /*
         NAME

            GetUserByName - Get a user by username.

         DESCRIPTION

            This method retrieves a user by the specified username and maps it to a UserDto object.
            It returns a 200 OK response with the user if found, or a 404 Not Found response if the user does not exist.

         PARAMETERS

            userName - A string representing the user's username.

         RETURNS

            Returns a 200 OK response with the user (UserDto) if found.
            Returns a 404 Not Found response if the user does not exist.
         */
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

        /*
         NAME

            GetUserByEmail - Get a user by email.

         DESCRIPTION

            This method retrieves a user by the specified email address and maps it to a UserDto object.
            It returns a 200 OK response with the user's ID if found, or a 404 Not Found response if the user does not exist.

         PARAMETERS

            userEmail - A string representing the user's email address.

         RETURNS

            Returns a 200 OK response with the user's ID (integer) if found.
            Returns a 404 Not Found response if the user does not exist.
         */
        [HttpGet("email/{userEmail}")]
        [ProducesResponseType(200, Type = typeof(int))]
        public IActionResult GetUserByEmail(string userEmail)
        {
            var user = _mapper.Map<UserDto>(_userRepository.GetUserByEmail(userEmail));
            if (user == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user.Id);

        }

        /*
         NAME

            GetCartIdByUserId - Get the cart ID associated with a user.

         DESCRIPTION

            This method retrieves the cart ID associated with a user by the specified user ID.
            It returns a 200 OK response with the cart ID (integer) if found, or a 404 Not Found response if the user does not exist.

         PARAMETERS

            userId - An integer representing the user's ID.

         RETURNS

            Returns a 200 OK response with the cart ID (integer) if found.
            Returns a 404 Not Found response if the user does not exist.
         */
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

        /*
         NAME

            SubstractFlex - Subtract flex dollars from a user's balance.

         DESCRIPTION

            This method subtracts a specified amount of flex dollars from a user's balance.
            It returns a 200 OK response if successful, or a 400 Bad Request response if the user does not exist or has insufficient flex dollars.

         PARAMETERS

            userId - An integer representing the user's ID.
            amount - An integer representing the amount of flex dollars to subtract.

         RETURNS

            Returns a 200 OK response if the flex dollars are successfully subtracted.
            Returns a 400 Bad Request response if the user does not exist or has insufficient flex dollars.
         */
        [HttpPost("{userId}/flex")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult SubstractFlex(int userId, [FromBody] double amount)
        {
            if ((bool)!_userRepository.UserExists(userId))
                return NotFound();
            User user = _userRepository.GetUser(userId);
            if (user.FlexDollars < amount)
                return BadRequest("Flex is not enough");
            else
            {
                user.FlexDollars -= (int)amount;
                if (!_userRepository.UpdateUser(user))
                    return BadRequest("Internal Server Error User's Flex could not be updated");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();

        }


        /*
         NAME

            CreateUser - Create a new user.

         DESCRIPTION

            This method creates a new user based on the provided UserDto object.
            It returns a 201 Created response if the user is successfully created, or returns appropriate error responses for validation or server errors.

         PARAMETERS

            userCreate - A UserDto object representing the user to be created.

         RETURNS

            Returns a 201 Created response if the user is successfully created.
            Returns a 400 Bad Request response for validation errors.
            Returns a 422 Unprocessable Entity response if the user already exists.
            Returns a 500 Internal Server Error response for server errors.
         */
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
            {
                return BadRequest(ModelState);
            }

            var existingUser = _userRepository.GetUsers()
                .FirstOrDefault(c => c.UserName.Trim().ToUpper() == userCreate.UserName.TrimEnd().ToUpper());

            if (existingUser != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = _mapper.Map<User>(userCreate);

            if (!_userRepository.CreateUser(newUser))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            try
            {
                // Create cart for the new user
                var cartDto = new CartDto { UserId = newUser.Id };
                var cartMap = _mapper.Map<Cart>(cartDto);
                if (!_cartRepository.CreateCart(cartMap))
                {
                    throw new Exception("Cart creation failed");
                }

                // Update the user with the cart ID
                newUser.CartId = _cartRepository.GetCartId(cartMap);
                if (!_userRepository.UpdateUser(newUser))
                {
                    throw new Exception("User update failed");
                }

                return Created("", newUser.Id); // Return 201 Created
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
        }

        /*
         NAME

            UpdateUser - Update a user's information.

         DESCRIPTION

            This method updates a user's information based on the provided UserDto object.
            It returns a 200 OK response if successful, or a 400 Bad Request response if the user does not exist or if the update fails.

         PARAMETERS

            userId - An integer representing the user's ID.
            updatedUser - A UserDto object representing the updated user information.

         RETURNS

            Returns a 200 OK response if the user's information is successfully updated.
            Returns a 400 Bad Request response if the user does not exist or if the update fails.
         */
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

        /*
         NAME

            DeleteOwner - Delete a user.

         DESCRIPTION

            This method deletes a user based on the provided user ID.
            It returns a 204 No Content response if successful, or returns appropriate error responses if the user does not exist or if the deletion fails.

         PARAMETERS

            userId - An integer representing the user's ID.

         RETURNS

            Returns a 204 No Content response if the user is successfully deleted.
            Returns a 400 Bad Request response if the user does not exist.
            Returns a 404 Not Found response if the user does not exist.
         */
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
