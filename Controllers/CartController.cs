using AutoMapper;
using FoodOrderingApi.Dto;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using FoodOrderingApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

        /*
        NAME

        GetCartByCartId - Retrieves a cart by its ID.

        SYNOPSIS

        [HttpGet("{cartId}")]
        [ProducesResponseType(200, Type = typeof(CartDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCartByCartId(int cartId)

        DESCRIPTION

        This method retrieves a cart by its ID and returns it as a CartDto. 
        It returns a 200 OK response with the cart if found, or a 400 Bad Request response if not found.

        PARAMETERS

        cartId - An integer representing the ID of the cart to retrieve.

        RETURNS

        Returns a 200 OK response with the CartDto if successful.
        Returns a 400 Bad Request response if the cart is not found.
        */
        [HttpGet("{cartId}")]
        [ProducesResponseType(200, Type = typeof(CartDto))]
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

        /*
        NAME

        GetCartById - Retrieves a cart by user ID.

        SYNOPSIS

        [HttpGet("{cartId}/user")]
        [ProducesResponseType(200, Type = typeof(CartDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCartById(int userId)

        DESCRIPTION

        This method retrieves a cart by user ID and returns it as a CartDto. 
        It returns a 200 OK response with the cart if found, or a 400 Bad Request response if not found.

        PARAMETERS

        userId - An integer representing the ID of the user to retrieve the cart for.

        RETURNS

        Returns a 200 OK response with the CartDto if successful.
        Returns a 400 Bad Request response if the user is not found.
        */
        [HttpGet("{cartId}/user")]
        [ProducesResponseType(200, Type = typeof(CartDto))]
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

        /*
        NAME

        GetCartByUserId - Retrieves a cart by user ID.

        SYNOPSIS

        [HttpGet("{userId}/cart")]
        [ProducesResponseType(200, Type = typeof(CartDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCartByUserId(int userId)

        DESCRIPTION

        This method retrieves a cart by user ID and returns it as a CartDto. 
        It returns a 200 OK response with the cart if found, or a 400 Bad Request response if not found.

        PARAMETERS

        userId - An integer representing the ID of the user to retrieve the cart for.

        RETURNS

        Returns a 200 OK response with the CartDto if successful.
        Returns a 400 Bad Request response if the user is not found.
        */
        [HttpGet("{userId}/cart")]
        [ProducesResponseType(200, Type = typeof(CartDto))]
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

        /*
        NAME

        AddMenuItemToCart - Adds a menu item to a cart.

        SYNOPSIS

        [HttpPost("add-to-cart")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult AddMenuItemToCart([FromBody] CartMenuItemInput input)

        DESCRIPTION

        This method adds a menu item to a cart.
        It returns a 204 No Content response if successful, a 404 Not Found response if the cart or menu item is not found,
        or a 400 Bad Request response if the request is invalid.

        PARAMETERS

        input - A CartMenuItemInput object containing the cart ID and menu item ID to add.

        RETURNS

        Returns a 204 No Content response if successful.
        Returns a 404 Not Found response if the cart or menu item is not found.
        Returns a 400 Bad Request response if the request is invalid.
        */
        [HttpPost("add-to-cart")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult AddMenuItemToCart([FromBody] CartMenuItemInput input)
        {
            var cart = _cartRepository.GetCart(input.CartId);

            if (cart == null)
            {
                return NotFound();
            }

            var menuItem = _menuItemRepository.GetMenuItem(input.MenuItemId);

            if (menuItem == null)
            {
                return BadRequest();
            }

            _cartRepository.AddMenuItemById(input.MenuItemId, cart);

            return NoContent();
        }

        public class CartMenuItemInput
        {
            public int CartId { get; set; }
            public int MenuItemId { get; set; }
        }

        /*
        NAME

        AddMenuItemByName - Adds a menu item to a cart by name.

        SYNOPSIS

        [HttpPost("{cartId}/menu-items/{name}")]
        public IActionResult AddMenuItemByName(int cartId, string name)

        DESCRIPTION

        This method adds a menu item to a cart by its name.
        It returns a 204 No Content response if successful, a 404 Not Found response if the cart or menu item is not found,
        or a 400 Bad Request response if the request is invalid.

        PARAMETERS

        cartId - An integer representing the ID of the cart.
        name - A string representing the name of the menu item to add.

        RETURNS

        Returns a 204 No Content response if successful.
        Returns a 404 Not Found response if the cart or menu item is not found.
        Returns a 400 Bad Request response if the request is invalid.
        */
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

        /*
        NAME

        RemoveMenuItem - Removes a menu item from a cart.

        SYNOPSIS

        [HttpDelete("{id}/menu-items/{menuItemId}")]
        public IActionResult RemoveMenuItem(int id, int menuItemId)

        DESCRIPTION

        This method removes a menu item from a cart.
        It returns a 204 No Content response if successful, a 404 Not Found response if the cart or menu item is not found,
        or a 400 Bad Request response if the request is invalid.

        PARAMETERS

        id - An integer representing the ID of the cart.
        menuItemId - An integer representing the ID of the menu item to remove.

        RETURNS

        Returns a 204 No Content response if successful.
        Returns a 404 Not Found response if the cart or menu item is not found.
        Returns a 400 Bad Request response if the request is invalid.
        */
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

        /*
        NAME

        Create - Creates a new cart.

        SYNOPSIS

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] CartDto cart)

        DESCRIPTION

        This method creates a new cart.
        It returns a 204 No Content response if successful, or a 400 Bad Request response if the request is invalid.

        PARAMETERS

        cart - A CartDto object representing the cart to create.

        RETURNS

        Returns a 204 No Content response if successful.
        Returns a 400 Bad Request response if the request is invalid.
        */
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] CartDto cart)
        {
            if (cart == null)
            {
                return BadRequest();
            }
            var cartMap = _mapper.Map<Cart>(cart);

            _cartRepository.CreateCart(cartMap);

            return CreatedAtAction(nameof(GetCartById), new { id = cart.Id }, cart.Id);

        }

        /*
        NAME

        Update - Updates an existing cart.

        SYNOPSIS

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Update(int id, [FromBody] Cart updatedCart)

        DESCRIPTION

        This method updates an existing cart.
        It returns a 204 No Content response if successful, a 404 Not Found response if the cart is not found,
        or a 400 Bad Request response if the request is invalid.

        PARAMETERS

        id - An integer representing the ID of the cart to update.
        updatedCart - A Cart object representing the updated cart data.

        RETURNS

        Returns a 204 No Content response if successful.
        Returns a 404 Not Found response if the cart is not found.
        Returns a 400 Bad Request response if the request is invalid.
        */
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

        /*
        NAME

        Delete - Deletes a cart.

        SYNOPSIS

        [HttpDelete]
        public IActionResult Delete([FromBody] Cart cart)

        DESCRIPTION

        This method deletes a cart.
        It returns an OK "success" response if successful.

        PARAMETERS

        cart - A Cart object representing the cart to delete.

        RETURNS

        Returns an OK "success" response if successful.
        */
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

        /*
        NAME

        GetCartItemsyCartId - Retrieves menu items in a cart by cart ID.

        SYNOPSIS

        [HttpGet("itemsinCart/{cartId}")]
        [ProducesResponseType(400)]
        public IActionResult GetCartItemsyCartId(int cartId)

        DESCRIPTION

        This method retrieves menu items in a cart by cart ID.
        It returns a list of menu items if successful, or a 400 Bad Request response if there's an exception.

        PARAMETERS

        cartId - An integer representing the ID of the cart.

        RETURNS

        Returns a list of menu items if successful.
        Returns a 400 Bad Request response if there's an exception.
        */
        [HttpGet("itemsinCart/{cartId}")]
        [ProducesResponseType(400)]
        public IActionResult GetCartItemsyCartId(int cartId)
        {
            try
            {
                var items = _cartRepository.GetMenuItemsInCart(cartId);

                return Ok((List<MenuItem>)items);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }
    }
}
