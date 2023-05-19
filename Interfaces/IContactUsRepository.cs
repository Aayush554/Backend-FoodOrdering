using FoodOrderingApi.Model;

namespace FoodOrderingApi.Interfaces
{
    public interface IContactUsRepository
    {
        ICollection<ContactUs> GetAllMessages();        
        string GetMessageByName(string fullName);
        string GetMessageByEmail(string email);
        string GetMessageByDateTime(DateTime dateTime);
        bool ContactUsExists(int contactUsId);
        bool CreateContactUs(ContactUs contactUs);
        bool UpdateContactUs(ContactUs contactUs);
        bool DeleteContactUs(ContactUs contactUs);
        bool Save();
    }
}
