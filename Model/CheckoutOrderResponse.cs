namespace FoodOrderingApi.Model
{
    /*
     NAME

        CheckoutOrderResponse - Represents the response after checking out an order.

     DESCRIPTION

        The CheckoutOrderResponse class defines the structure of the response after a successful order checkout.
        It includes properties for the session ID and public key, which may be used for payment processing.

     PROPERTIES

        - SessionId (string?): The session ID associated with the order checkout.
        - PubKey (string?): The public key used for payment processing.

     */
    public class CheckoutOrderResponse
    {
        public string? SessionId { get; set; }
        public string? PubKey { get; set; }
    }
}
