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
    public class CartController : Controller
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private IUserRepository _userRepository;
        private ICartRepository _cartRepository;
        private IMapper _mapper;

        public CartController(ICartRepository crateRepository, IMapper mapper, IUserRepository userRepository,
            IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
            _userRepository = userRepository;
            _cartRepository = crateRepository;
            _mapper = mapper;
        }

        [HttpGet("{cartId}")]
        [ProducesResponseType(200, Type = typeof(Cart))]
        [ProducesResponseType(400)]
        public IActionResult GetCartByCartId(int cartId)
        {
            if ((bool)!_cartRepository.CartExists(cartId))
                return NotFound();

            var cart = _mapper.Map<CartDto>(_cartRepository.GetCart(cartId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cart);
        }

        [HttpGet("{cartId}/user")]
        [ProducesResponseType(200, Type = typeof(Cart))]
        [ProducesResponseType(400)]
        public IActionResult GetCartById(int userId)
        {
            if ((bool)!_userRepository.UserExists(userId))
                return NotFound();

            var cart = _mapper.Map<CartDto>(_cartRepository.GetCartByUserId(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cart);
        }

        [HttpGet("{userId}/cart")]
        [ProducesResponseType(200, Type = typeof(Cart))]
        [ProducesResponseType(400)]
        public IActionResult GetCartByUserId(int userId)
        {
            if ((bool)!_userRepository.UserExists(userId))
                return NotFound();

            var cart = _mapper.Map<CartDto>(_cartRepository.GetCartByUserId(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cart);
        }
        [HttpPost("{menuItemId}/cart")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult AddMenuItem(int id, [FromBody] int menuItemId)
        {
            var cart = _cartRepository.GetCart(id);

            if (cart == null)
            {
                return NotFound();
            }

            var menuItem = _menuItemRepository.GetMenuItem(menuItemId);

            if (menuItem == null)
            {
                return BadRequest();
            }

            _cartRepository.AddMenuItemById(menuItemId, cart);

            return NoContent();
        }

        [HttpPost("{cartId}/menu-items/{name}")]
        public IActionResult AddMenuItemByName(int cartId, string name)
        {
            var cart = _cartRepository.GetCart(cartId);

            if (cart == null)
            {
                return NotFound();
            }

            var menuItem = _menuItemRepository.GetMenuItemByName(name);

            if (menuItem == null)
            {
                return BadRequest();
            }

            _cartRepository.AddMenuItemByName(name, cart);

            return NoContent();
        }

        [HttpDelete("{id}/menu-items/{menuItemId}")]
        public IActionResult RemoveMenuItem(int id, int menuItemId)
        {
            var cart = _cartRepository.GetCart(id);

            if (cart == null)
            {
                return NotFound();
            }

            var menuItem = cart.CartItems.FirstOrDefault(ci => ci.MenuItem.Id == menuItemId)?.MenuItem.Id;

            if (menuItem == null)
            {
                return BadRequest();
            }

            _cartRepository.RemoveMenuItem((int)menuItem, cart);

            return NoContent();
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] Cart cart)
        {
            if (cart == null)
            {
                return BadRequest();
            }

            _cartRepository.CreateCart(cart);

            return Ok("success");
        }
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Update(int id, [FromBody] Cart updatedCart)
        {
            if (updatedCart == null || updatedCart.Id != id)
            {
                return BadRequest();
            }

            var existingCart = _cartRepository.GetCart(id);

            if (existingCart == null)
            {
                return NotFound();
            }

            existingCart.UserId = updatedCart.UserId;

            _cartRepository.UpdateCart(existingCart);

            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] Cart cart)
        {
            if (cart == null)
            {
                return BadRequest();
            }

            _cartRepository.DeleteCart(cart);

            return Ok("success");
        }

    }
}








    
