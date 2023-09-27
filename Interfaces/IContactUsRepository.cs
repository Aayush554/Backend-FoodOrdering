using FoodOrderingApi.Model;
using System;
using System.Collections.Generic;

namespace FoodOrderingApi.Interfaces
{
    public interface IContactUsRepository
    {
        /*
         NAME

            GetAllMessages - Retrieve all contact messages.

         SYNOPSIS

            ICollection<ContactUs> GetAllMessages()

         DESCRIPTION

            This function retrieves a collection of all contact messages.

         RETURNS

            Returns a collection of all contact messages.
        */
        ICollection<ContactUs> GetAllMessages();

        /*
         NAME

            GetMessageByName - Retrieve a contact message by full name.

         SYNOPSIS

            string GetMessageByName(string fullName)

         DESCRIPTION

            This function retrieves a contact message by the full name of the sender.

         PARAMETERS

            fullName - The full name of the sender.

         RETURNS

            Returns the contact message associated with the provided full name.
        */
        string GetMessageByName(string fullName);

        /*
         NAME

            GetMessageByEmail - Retrieve a contact message by email.

         SYNOPSIS

            string GetMessageByEmail(string email)

         DESCRIPTION

            This function retrieves a contact message by the sender's email address.

         PARAMETERS

            email - The email address of the sender.

         RETURNS

            Returns the contact message associated with the provided email address.
        */
        string GetMessageByEmail(string email);

        /*
         NAME

            GetMessageByDateTime - Retrieve a contact message by date and time.

         SYNOPSIS

            string GetMessageByDateTime(DateTime dateTime)

         DESCRIPTION

            This function retrieves a contact message by the date and time it was sent.

         PARAMETERS

            dateTime - The date and time when the message was sent.

         RETURNS

            Returns the contact message associated with the provided date and time.
        */
        string GetMessageByDateTime(DateTime dateTime);

        /*
         NAME

            ContactUsExists - Check if a contact message exists.

         SYNOPSIS

            bool ContactUsExists(int contactUsId)

         DESCRIPTION

            This function checks if a contact message exists based on its unique identifier.

         PARAMETERS

            contactUsId - The unique identifier of the contact message.

         RETURNS

            Returns true if the contact message exists; otherwise, false.
        */
        bool ContactUsExists(int contactUsId);

        /*
         NAME

            CreateContactUs - Create a new contact message.

         SYNOPSIS

            bool CreateContactUs(ContactUs contactUs)

         DESCRIPTION

            This function creates a new contact message.

         PARAMETERS

            contactUs - The contact message to be created.

         RETURNS

            Returns true if the contact message was successfully created; otherwise, false.
        */
        bool CreateContactUs(ContactUs contactUs);

        /*
         NAME

            UpdateContactUs - Update an existing contact message.

         SYNOPSIS

            bool UpdateContactUs(ContactUs contactUs)

         DESCRIPTION

            This function updates an existing contact message.

         PARAMETERS

            contactUs - The contact message to be updated.

         RETURNS

            Returns true if the contact message was successfully updated; otherwise, false.
        */
        bool UpdateContactUs(ContactUs contactUs);

        /*
         NAME

            DeleteContactUs - Delete a contact message.

         SYNOPSIS

            bool DeleteContactUs(ContactUs contactUs)

         DESCRIPTION

            This function deletes a contact message.

         PARAMETERS

            contactUs - The contact message to be deleted.

         RETURNS

            Returns true if the contact message was successfully deleted; otherwise, false.
        */
        bool DeleteContactUs(ContactUs contactUs);

        /*
         NAME

            Save - Save changes to the data store.

         SYNOPSIS

            bool Save()

         DESCRIPTION

            This function saves any changes made to the data store.

         RETURNS

            Returns true if changes were successfully saved; otherwise, false.
        */
        bool Save();
    }
}
