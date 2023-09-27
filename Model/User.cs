using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingApi.Model
{
    /*
     NAME

        User - Represents a registered user of the system.

     DESCRIPTION

        The User class defines the structure of a registered user in the system.
        It includes properties for user information such as ID, name, username, email,
        password hash, phone number, address, postal code, image URL, flex dollars,
        and registration date. It also includes navigation properties for orders, reviews,
        a shopping cart, and payments associated with the user.

     PROPERTIES

        - Id (int): The unique identifier for the user.
        - Name (string): The user's full name.
        - UserName (string): The user's username.
        - Email (string): The user's email address.
        - PasswordHash (string): The hashed password of the user.
        - Phone (string): The user's phone number.
        - Address (string): The user's address.
        - PostCode (string): The user's postal code.
        - ImageUrl (string): The URL to the user's profile image.
        - FlexDollars (int): The amount of flex dollars associated with the user.
        - CreatedDate (DateTime): The date when the user's account was created.
        - Orders (List<Order>): A list of orders placed by the user.
        - Reviews (List<Review>): A list of reviews written by the user.
        - CartId (int): The ID of the user's shopping cart.
        - Cart (Cart): The user's shopping cart.
        - Payments (List<Payment>): A list of payments made by the user.

     */
    public class User
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
        public string? ImageUrl { get; set; }
        public int? FlexDollars { get; set; }
        public DateTime? CreatedDate { get; set; }

        // Navigation properties
        [ForeignKey("Order")]
        public List<Order>? Orders { get; set; }

        [ForeignKey("Review")]
        public List<Review>? Reviews { get; set; }

        [ForeignKey("Cart")]
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }

        [ForeignKey("Payment")]
        public List<Payment>? Payments { get; set; }
    }
}
