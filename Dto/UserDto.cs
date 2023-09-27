/*
 * NAME
 * 
 * UserDto - Data Transfer Object (DTO) for representing a user.
 * 
 * DESCRIPTION
 * 
 * The UserDto class represents a Data Transfer Object (DTO) for users in the Food Ordering API. DTOs are used to transfer data between different parts of an application, typically between the client and server. This class contains properties that mirror the structure of a user entity, allowing for efficient data transfer.
 */
namespace FoodOrderingApi.Dto
{
    public class UserDto
    {
        /*
         * PROPERTY
         * 
         * Id (int?): Gets or sets the unique identifier of the user.
         */
        public int? Id { get; set; }

        /*
         * PROPERTY
         * 
         * Name (string?): Gets or sets the name of the user.
         */
        public string? Name { get; set; }

        /*
         * PROPERTY
         * 
         * UserName (string?): Gets or sets the username of the user.
         */
        public string? UserName { get; set; }

        /*
         * PROPERTY
         * 
         * Email (string?): Gets or sets the email address of the user.
         */
        public string? Email { get; set; }

        /*
         * PROPERTY
         * 
         * Phone (string?): Gets or sets the phone number of the user.
         */
        public string? Phone { get; set; }

        /*
         * PROPERTY
         * 
         * Address (string?): Gets or sets the address of the user.
         */
        public string? Address { get; set; }

        /*
         * PROPERTY
         * 
         * PostCode (string?): Gets or sets the postal code of the user.
         */
        public string? PostCode { get; set; }

        /*
         * PROPERTY
         * 
         * ImageUrl (string?): Gets or sets the URL of the user's profile image.
         */
        public string? ImageUrl { get; set; }

        /*
         * PROPERTY
         * 
         * CreatedDate (DateTime?): Gets or sets the date when the user's account was created.
         */
        public DateTime? CreatedDate { get; set; }

        /*
         * PROPERTY
         * 
         * CartId (int?): Gets or sets the identifier of the user's shopping cart.
         */
        public int? CartId { get; set; }

        /*
         * PROPERTY
         * 
         * FlexDollars (int?): Gets or sets the amount of Flex Dollars associated with the user.
         */
        public int? FlexDollars { get; set; }
    }
}
