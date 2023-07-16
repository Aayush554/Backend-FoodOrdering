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

    [HttpPost("checkout")]
    public async Task<IActionResult> CheckoutOrder([FromBody] MenuItemDto[] products)
    {
        try
        {

            decimal totalPrice = CalculateOrderAmount(products) * 100;

            var taxPrice = 0;
            var shippingPrice = 0;

            // Save the order to the database if needed
            // need to update a lot in the database
            // need to create a  user eith related cart and related 

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