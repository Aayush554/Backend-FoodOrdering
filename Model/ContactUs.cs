using System;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApi.Model
{
    /*
     NAME

        ContactUs - Represents a contact us message.

     DESCRIPTION

        The ContactUs class defines the structure of a contact us message.
        It includes properties for the message's ID, sender's full name, email address, subject, message content, and creation date.

     PROPERTIES

        - Id (int?): The unique identifier for the contact us message.
        - FullName (string?): The sender's full name.
        - Email (string?): The sender's email address (must be a valid email format).
        - Subject (string?): The subject of the message.
        - Message (string?): The content of the message.
        - CreatedAt (DateTime?): The date and time when the message was created (defaults to UTC time).

     VALIDATION

        - FullName: Required and limited to 100 characters.
        - Email: Required, must be a valid email address, and limited to 100 characters.
        - Subject: Required and limited to 100 characters.
        - Message: Required and limited to 500 characters.

     */
    public class ContactUs
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? FullName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(100)]
        public string? Subject { get; set; }

        [Required]
        [StringLength(500)]
        public string? Message { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
