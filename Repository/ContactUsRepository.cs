using AutoMapper;
using FoodOrderingApi.Data;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using System;

namespace FoodOrderingApi.Repository
{
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ContactUsRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /*
        NAME

           ContactUsExists - Checks if a contact message with the specified ID exists.

        DESCRIPTION

           This method checks if a contact message with the specified ID exists in the data context.

        PARAMETERS

           contactUsId - An integer representing the ID of the contact message to check.

        RETURNS

           Returns true if a contact message with the specified ID exists; otherwise, false.
        */
        public bool ContactUsExists(int contactUsId)
        {
            return _context.ContactUs.Any(c => c.Id == contactUsId);
        }
                /*
         NAME

            CreateContactUs - Creates a new contact message.

         DESCRIPTION

            This method adds a new contact message to the data context.

         PARAMETERS

            contactUs - The ContactUs object representing the message to be created.

         RETURNS

            Returns true if the contact message creation was successful; otherwise, false.
         */
        public bool CreateContactUs(ContactUs contactUs)
        {
            _context.Add(contactUs);
            return Save();
        }

                /*
         NAME

            DeleteContactUs - Deletes a contact message.

         DESCRIPTION

            This method removes a contact message from the data context.

         PARAMETERS

            contactUs - The ContactUs object representing the message to be deleted.

         RETURNS

            Returns true if the contact message deletion was successful; otherwise, false.
         */
        public bool DeleteContactUs(ContactUs contactUs)
        {
            _context.Remove(contactUs);
            return Save();
        }

                /*
         NAME

            GetAllMessages - Retrieves all contact messages.

         DESCRIPTION

            This method retrieves a collection of all contact messages from the data context.

         RETURNS

            Returns a collection of ContactUs objects representing all contact messages.
         */
        public ICollection<ContactUs> GetAllMessages()
        {
            return (ICollection<ContactUs>)_context.ContactUs.Select(c => c.Message).ToList();
        }


        /*
         NAME

            GetMessageByDateTime - Retrieves a contact message by DateTime.

         DESCRIPTION

            This method retrieves a contact message with the specified DateTime value from the data context.

         PARAMETERS

            dateTime - A DateTime value representing the date and time of the contact message.

         RETURNS

            Returns the message content of the contact message with the specified DateTime.
         */
        public string GetMessageByDateTime(DateTime dateTime)
        {
            return _context.ContactUs.Where(c => c.CreatedAt == dateTime).Select(c => c.Message).FirstOrDefault();

        }

                /*
         NAME

            GetMessageByEmail - Retrieves a contact message by email.

         DESCRIPTION

            This method retrieves a contact message with the specified email address from the data context.

         PARAMETERS

            email - A string representing the email address associated with the contact message.

         RETURNS

            Returns the message content of the contact message with the specified email address.
         */
        public string GetMessageByEmail(string email)
        {
            return _context.ContactUs.Where(c => c.Email == email).Select(c => c.Message).FirstOrDefault();

        }

                /*
         NAME

            GetMessageByName - Retrieves a contact message by full name.

         DESCRIPTION

            This method retrieves a contact message with the specified full name from the data context.

         PARAMETERS

            fullName - A string representing the full name associated with the contact message.

         RETURNS

            Returns the message content of the contact message with the specified full name.
         */
        public string GetMessageByName(string fullName)
        {
            return _context.ContactUs.Where(c => c.FullName == fullName).Select(c => c.Message).FirstOrDefault();

        }

                /*
         NAME

            Save - Saves changes to the data context.

         DESCRIPTION

            This method saves changes made to the data context.

         RETURNS

            Returns true if the save operation was successful; otherwise, false.
         */
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

                /*
         NAME

            UpdateContactUs - Updates a contact message.

         DESCRIPTION

            This method updates a contact message in the data context.

         PARAMETERS

            contactUs - The ContactUs object representing the message to be updated.

         RETURNS

            Returns true if the update operation was successful; otherwise, false.
         */
        public bool UpdateContactUs(ContactUs contactUs)
        {
            _context.Update(contactUs);
            return Save();
        }
    }
}
