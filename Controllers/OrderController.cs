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
        private IPaymentRepository _paymentRepository;
        private IUserRepository _userRepository;
        private IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository, IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        public IActionResult GetOrder()
        {
            var menuItems = _mapper.Map<List<MenuItemDto>>(_orderRepository.GetAllOrders());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(menuItems);
        }

        [HttpGet("{userId}/orders")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        [ProducesResponseType(400)]
        public IActionResult GetOrdersById(int orderId)
        {
            if (!_orderRepository.OrderExists(orderId))
                return NotFound();

            var owner = _mapper.Map<OrderDto>(_orderRepository.GetOrdersById(orderId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }
        [HttpGet("{userName}/orders")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        [ProducesResponseType(400)]
        public IActionResult GetOrdersByName(string userName)
        {

            var owner = _mapper.Map<OrderDto>(_orderRepository.GetOrderByName(userName));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }
        public class OrderRequestModel
        {
            public OrderDto OrderCreate { get; set; }
            public Payment PaymentCreate { get; set; }
            public CartDto CartBought { get; set; }
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrder([FromBody] OrderRequestModel orderRequest)
        {
            // validate user
            // validate cart items and order

            if (orderRequest.OrderCreate == null)
                return BadRequest(ModelState);
            var order = _orderRepository.GetAllOrders()
                .Where(c => c.Id == orderRequest.OrderCreate.Id)
                .FirstOrDefault();
            var users = _userRepository.GetUser((int)orderRequest.OrderCreate.UserId);
            if (users == null)
            {
                ModelState.AddModelError("", "User does not exists");
                return StatusCode(422, ModelState);
            }
            // payment validation
            // decrease the quantity
            // 
            var paymnet = _paymentRepository.CreatePayment(orderRequest.PaymentCreate);
            if (users == null)
            {

                ModelState.AddModelError("", "User does not exists");
                return StatusCode(422, ModelState);
            }
            if (order != null)
            {
                ModelState.AddModelError("", "Order already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(orderRequest.OrderCreate);



            if (!_orderRepository.CreateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{orderId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner([FromQuery]int orderId, [FromBody] OrderDto updatedOrder)
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
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

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
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}
