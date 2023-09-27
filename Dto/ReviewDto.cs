/*
 * NAME
 * 
 * ReviewDto - Data Transfer Object (DTO) for representing a review.
 * 
 * DESCRIPTION
 * 
 * The ReviewDto class represents a Data Transfer Object (DTO) for reviews in the Food Ordering API. DTOs are used to transfer data between different parts of an application, typically between the client and server. This class contains properties that mirror the structure of a review entity, allowing for efficient data transfer.
 */
namespace FoodOrderingApi.Dto
{
    public class ReviewDto
    {
        /*
         * PROPERTY
         * 
         * Id (int?): Gets or sets the unique identifier of the review.
         */
        public int? Id { get; set; }

        /*
         * PROPERTY
         * 
         * Rating (int?): Gets or sets the rating associated with the review.
         */
        public int? Rating { get; set; }

        /*
         * PROPERTY
         * 
         * ReviewMessage (string?): Gets or sets the text message of the review.
         */
        public string? ReviewMessage { get; set; }

        /*
         * PROPERTY
         * 
         * ReviewDate (DateTime): Gets or sets the date and time when the review was posted.
         */
        public DateTime ReviewDate { get; set; }

        /*
         * PROPERTY
         * 
         * MenuItemId (int?): Gets or sets the identifier of the menu item associated with the review.
         */
        public int? MenuItemId { get; set; }

        /*
         * PROPERTY
         * 
         * UserId (int?): Gets or sets the identifier of the user who posted the review.
         */
        public int? UserId { get; set; }
    }
}
