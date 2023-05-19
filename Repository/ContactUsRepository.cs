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
        public bool ContactUsExists(int contactUsId)
        {
            return _context.ContactUs.Any(c => c.Id == contactUsId);
        }

        public bool CreateContactUs(ContactUs contactUs)
        {
            _context.Add(contactUs);
            return Save();
        }

        public bool DeleteContactUs(ContactUs contactUs)
        {
            _context.Remove(contactUs);
            return Save();
        }

        public ICollection<ContactUs> GetAllMessages()
        {
            return (ICollection<ContactUs>)_context.ContactUs.Select(c => c.Message).ToList();
        }

        public string GetMessageByDateTime(DateTime dateTime)
        {
            return _context.ContactUs.Where(c => c.CreatedAt == dateTime).Select(c => c.Message).FirstOrDefault();

        }

        public string GetMessageByEmail(string email)
        {
            return _context.ContactUs.Where(c => c.Email == email).Select(c => c.Message).FirstOrDefault();

        }

        public string GetMessageByName(string fullName)
        {
            return _context.ContactUs.Where(c => c.FullName == fullName).Select(c => c.Message).FirstOrDefault();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateContactUs(ContactUs contactUs)
        {
            _context.Update(contactUs);
            return Save();
        }
    }
}
