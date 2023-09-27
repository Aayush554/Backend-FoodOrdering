using AutoMapper;
using FoodOrderingApi.Dto;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using FoodOrderingApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Stripe;

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
            StripeConfiguration.ApiKey = "sk_test_51NNQHnFxwsL2TMdN5EGJGmUld4G6UU5nuKeMciYaY18LHv6KtW32oLOeT0oCg90BeypO315AlZlgLIQqKGhI417h00cYvrNc6t";

        }
            /*
        NAME

            IActionResult::CreatePayment - Creates a new payment record.

        SYNOPSIS

            [HttpPost("payment")]
            [ProducesResponseType(204)]
            [ProducesResponseType(400)]
            public IActionResult CreatePayment([FromBody] PaymentDto paymentCreate)

        DESCRIPTION

            This method allows the creation of a new payment record based on the provided PaymentDto. It maps the incoming DTO to a Payment entity and adds it to the payment repository.

        PARAMETERS

            - paymentCreate (PaymentDto): The PaymentDto containing payment details.

        RETURNS

            - 204 (NoContent): If the payment record was successfully created.
            - 400 (BadRequest): If the provided paymentCreate parameter is invalid or missing.

        USAGE

            This endpoint is typically used to create a new payment record in the system.

        SEE ALSO

            - PaymentDto: The DTO containing payment information.
    */
        [HttpPost("payment")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePayment([FromBody] PaymentDto paymentCreate)
        {

            var payment = _mapper.Map<Payment>(paymentCreate);
            _paymentRepository.CreatePayment(payment);

            return Ok("Successfully created");
        }
        /*
        NAME

            IActionResult::UpdatePayment - Updates an existing payment record.

        SYNOPSIS

            [HttpPut("{userId}/payment")]
            public IActionResult UpdatePayment(int userId, [FromBody] PaymentDto paymentUpdateDto)

        DESCRIPTION

            This method allows updating an existing payment record associated with the specified user ID. It retrieves the existing payment, maps the incoming DTO to it, and updates the payment repository.

        PARAMETERS

            - userId (int): The user ID associated with the payment record to be updated.
            - paymentUpdateDto (PaymentDto): The PaymentDto containing updated payment details.

        RETURNS

            - 204 (NoContent): If the payment record was successfully updated.
            - 404 (NotFound): If the specified payment or user does not exist.

        USAGE

            This endpoint is typically used to update an existing payment record for a user.

        SEE ALSO

            - PaymentDto: The DTO containing payment information.
        */

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

        /*
        NAME

            IActionResult::DeletePayment - Deletes an existing payment record.

        SYNOPSIS

            [HttpDelete("{paymentId}")]
            [ProducesResponseType(400)]
            [ProducesResponseType(204)]
            [ProducesResponseType(404)]
            public IActionResult DeletePayment(int paymentId)

        DESCRIPTION

            This method allows the deletion of an existing payment record with the specified payment ID. It first checks if the payment exists, then attempts to delete it from the payment repository.

        PARAMETERS

            - paymentId (int): The unique identifier of the payment record to be deleted.

        RETURNS

            - 204 (NoContent): If the payment record was successfully deleted.
            - 404 (NotFound): If the specified payment does not exist.

        USAGE

            This endpoint is typically used to delete an existing payment record from the system.

        SEE ALSO

            - Payment: The entity representing payment records.
        */
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
                ModelState.AddModelError("", "Something went wrong deleting payment history");
            }

            return NoContent();
        }

    }
}
