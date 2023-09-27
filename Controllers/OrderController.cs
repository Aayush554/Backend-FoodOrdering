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
    public class OrderController : Controller
    {

        private readonly IMapper _mapper;
        private ICartRepository _cartRepository;
        private IPaymentRepository _paymentRepository;
        private IUserRepository _userRepository;
        private IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository, IPaymentRepository paymentRepository, ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _paymentRepository = paymentRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        /*
            NAME

            IActionResult::GetOrder - Retrieve a list of all orders.

            SYNOPSIS

            [HttpGet]
            public IActionResult GetOrder()

            DESCRIPTION

            This function retrieves a list of all orders and returns them as OrderedItems.
            It returns a 200 OK response with the list of orders if successful.

            RETURNS

            Returns a 200 OK response with a JSON array of OrderedItems if successful.
            Returns a BadRequest response if the ModelState is invalid.
        */
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderedItems>))]
        public IActionResult GetOrder()
        {
            List<OrderedItems> orders = _orderRepository.GetAllOrders().ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        /*
            NAME

            IActionResult::GetOrdersById - Retrieve orders for a specific user by their userId.

            SYNOPSIS

            [HttpGet("{userId}/orders")]
            public IActionResult GetOrdersById(int userId)

            DESCRIPTION

            This function retrieves orders for a specific user identified by their userId.
            It returns a 200 OK response with the user's orders if successful.

            PARAMETERS

            int userId - The userId of the user for whom to retrieve orders.

            RETURNS

            Returns a 200 OK response with a JSON array of Order objects if successful.
            Returns a BadRequest response if the ModelState is invalid.
        */
        [HttpGet("{userId}/orders")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        [ProducesResponseType(400)]
        public IActionResult GetOrdersById(int userId)
        {
            // Get all the orders related to the specified user.
            // If there are any orders, it sends the results.

            var owner = _mapper.Map<OrderDto>(_orderRepository.GetOrdersById(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        /*
            NAME

            IActionResult::CreateOrder - Create a new order.

            SYNOPSIS

            [HttpPost]
            public IActionResult CreateOrder([FromBody] OrderDto orderCreate)

            DESCRIPTION

            This function creates a new order based on the provided OrderDto.
            It performs validations, associates the order with a user, and saves it to the repository.
            It returns a 201 Created response with the order's ID if successful.

            PARAMETERS

            [FromBody] OrderDto orderCreate - The data for creating the new order.

            RETURNS

            Returns a 201 Created response with the newly created order's ID if successful.
            Returns BadRequest responses for validation errors or if the ModelState is invalid.
            Returns a 422 Unprocessable Entity response if the user or cart does not exist.
        */
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateOrder([FromBody] OrderDto orderCreate)
        {
            if (orderCreate == null)
            {
                return BadRequest(ModelState);
            }
            var users = _userRepository.GetUser((int)orderCreate.UserId);
            if (users == null)
            {
                ModelState.AddModelError("", "User does not exist");
                return StatusCode(422, ModelState);
            }
            Cart cart = _cartRepository.GetCart((int)orderCreate.CartId);
            if (cart == null)
            {
                ModelState.AddModelError("", "Cart does not exist");
                return StatusCode(422, ModelState);
            }

            var orderRequest = _mapper.Map<Order>(orderCreate);

            if (orderRequest == null)
                return BadRequest(ModelState);

            orderRequest.User = _userRepository.GetUser((int)orderRequest.UserId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_orderRepository.CreateOrder(orderRequest))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Created("", orderRequest.Id);
        }

        /*
            NAME

            IActionResult::CreateOrder - Create a new order for a user.

            SYNOPSIS

            [HttpPost("{userId}/order")]
            public IActionResult CreateOrder(int userId)

            DESCRIPTION

            This function creates a new order for a user identified by their userId.
            It associates the order with the user and saves it to the repository.
            It returns a 201 Created response with the order's ID if successful.

            PARAMETERS

            int userId - The userId of the user for whom to create the order.

            RETURNS

            Returns a 201 Created response with the newly created order's ID if successful.
            Returns BadRequest responses for validation errors or if the ModelState is invalid.
        */
        [HttpPost("{userId}/order")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateOrder(int userId)
        {
            int cartId = (int)_userRepository.GetCartIdByUserId(userId);
            var user = _userRepository.GetUser(userId);

            Order order = new Order
            {
                UserId = userId,
                User = user,
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_orderRepository.CreateOrder(order))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Created("", order.Id);
        }

        /*
            NAME

            IActionResult::UpdateOrder - Update an existing order.

            SYNOPSIS

            [HttpPut("{orderId}")]
            public IActionResult UpdateOrder([FromQuery]int orderId, [FromBody] OrderDto updatedOrder)

            DESCRIPTION

            This function updates an existing order based on the provided OrderDto and orderId.
            It performs validations and updates the order's properties.
            It returns a 204 No Content response if successful.

            PARAMETERS

            [FromQuery]int orderId - The ID of the order to be updated.
            [FromBody] OrderDto updatedOrder - The data for updating the order.

            RETURNS

            Returns a 204 No Content response if the update is successful.
            Returns BadRequest responses for validation errors or if the ModelState is invalid.
            Returns a 404 Not Found response if the order does not exist.
        */
        [HttpPut("{orderId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOrder([FromQuery] int orderId, [FromBody] OrderDto updatedOrder)
        {
            if (updatedOrder == null)
                return BadRequest(ModelState);

            if (orderId != updatedOrder.Id)
                return BadRequest(ModelState);

            if (!_orderRepository.OrderExists(orderId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var orderMap = _mapper.Map<Order>(updatedOrder);

            if (!_orderRepository.UpdateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong updating order");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /*
            NAME

            IActionResult::DeleteOwner - Delete an existing order.

            SYNOPSIS

            [HttpDelete("{orderId}")]
            public IActionResult DeleteOwner(int orderId)

            DESCRIPTION

            This function deletes an existing order based on the provided orderId.
            It performs validations and deletes the order from the repository.
            It returns a 204 No Content response if successful.

            PARAMETERS

            int orderId - The ID of the order to be deleted.

            RETURNS

            Returns a 204 No Content response if the deletion is successful.
            Returns a 404 Not Found response if the order does not exist.
        */
        [HttpDelete("{orderId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int orderId)
        {
            if (!_orderRepository.OrderExists(orderId))
            {
                return NotFound();
            }

            var ownerToDelete = _orderRepository.GetOrder(orderId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_orderRepository.DeleteOrder(ownerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting order");
            }

            return NoContent();
        }
    }
}