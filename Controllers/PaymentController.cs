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
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePayment([FromBody] PaymentDto paymentCreate)
        {

            var payment = _mapper.Map<Payment>(paymentCreate);
            _paymentRepository.CreatePayment(payment);

            return Ok("Successfully created");
        }
        [HttpPut("{userId}/payment")]
        public IActionResult UpdatePayment(int userId, [FromBody] PaymentDto paymentUpdateDto)
        {
            var payment = _paymentRepository.GetPaymentByUserId(userId);

            if (payment == null)
            {
                return NotFound();
            }

            _mapper.Map(paymentUpdateDto, payment);

            _paymentRepository.UpdatePayment(payment);

            return NoContent();
        }
        [HttpDelete("{paymentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePayment(int paymentId)
        {
            if (!_paymentRepository.PaymentExists(paymentId))
            {
                return NotFound();
            }

            var payment = _paymentRepository.GetPayment(paymentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_paymentRepository.DeletePayment(payment))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}
