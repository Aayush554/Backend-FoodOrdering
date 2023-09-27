using FoodOrderingApi.Model;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using FoodOrderingApi.Dto;

namespace FoodOrderingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CheckoutController : Controller
{
    private readonly IConfiguration _configuration;

    private static string s_wasmClientURL = string.Empty;

    public CheckoutController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /*
      NAME

      IActionResult::CheckoutOrder - Process the checkout of an order for a list of MenuItems.

      SYNOPSIS

      public async Task<IActionResult> CheckoutOrder([FromBody] MenuItemDto[] products)

      DESCRIPTION

      This asynchronous function handles the checkout process for a list of MenuItems.
      It calculates the total order amount, sets up payment using Stripe, and returns a client secret for payment.

      PARAMETERS

      [FromBody] MenuItemDto[] products - An array of MenuItemDto objects representing the selected MenuItems.

      RETURNS

      Returns an Ok response with a JSON object containing the ClientSecret for payment if successful.
      Returns a BadRequest response with an error message if an exception occurs during the checkout process.
  */
    [HttpPost("checkout")]
    public async Task<IActionResult> CheckoutOrder([FromBody] MenuItemDto[] products)
    {
        try
        {
            decimal totalPrice = CalculateOrderAmount(products) * 100;

            var taxPrice = 0;
            var shippingPrice = 0;


            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long?)totalPrice,
                Currency = "usd"
            };
            var service = new PaymentIntentService();
            var paymentIntent = service.Create(options);

            return Ok(new { ClientSecret = paymentIntent.ClientSecret });
        }
        catch (Exception e)
        {
            return BadRequest(new { error = new { message = e.Message } });
        }
    }

    /*
        NAME

        CalculateOrderAmount - Calculate the total order amount based on selected MenuItems.

        SYNOPSIS

        private decimal CalculateOrderAmount(MenuItemDto[] products)

        DESCRIPTION

        This private function calculates the total order amount based on the prices of the selected MenuItems.

        PARAMETERS

        MenuItemDto[] products - An array of MenuItemDto objects representing the selected MenuItems.

        RETURNS

        Returns the total order amount as a decimal.
    */
    private decimal CalculateOrderAmount(MenuItemDto[] products)
    {
        decimal total = 0;

        foreach (var product in products)
        {
            total = (decimal)(total + product.Price);
        }
        return total;
    }
}
