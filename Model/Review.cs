using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingApi.Model
{
    /*
     NAME

        Review - Represents a user review for a menu item.

     DESCRIPTION

        The Review class defines the structure of a user review for a menu item in the system.
        It includes properties for the review ID, rating, review message, review date,
        associated menu item, and user.

     PROPERTIES

        - Id (int): The unique identifier for the review.
        - Rating (int): The rating given by the user for the menu item.
        - ReviewMessage (string): The user's written review message.
        - ReviewDate (DateTime): The date when the review was created.
        - MenuItemId (int): The ID of the associated menu item.
        - MenuItem (MenuItem): The menu item that the review is associated with.
        - UserId (int): The ID of the user who wrote the review.
        - User (User): The user who wrote the review.

     */
    public class Review
    {
        public int? Id { get; set; }
        public int? Rating { get; set; }
        public string? ReviewMessage { get; set; }
        public DateTime ReviewDate { get; set; }

        [ForeignKey("MenuItem")]
        public int? MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}

