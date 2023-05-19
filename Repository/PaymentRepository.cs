using AutoMapper;
using FoodOrderingApi.Data;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;

namespace FoodOrderingApi.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PaymentRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public Payment GetPaymentByUserId(int userId)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            return GetPayment((int)user.Id);
        }

        public bool CreatePayment(Payment payment)
        {
            _context.Add(payment);
            return Save();
        }

        public bool DeletePayment(Payment payment)
        {
            _context.Remove(payment);
            return Save();
        }

        public bool PaymentExistsByUserId(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).Select(p => p.PaymentId) == null ? false : true;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePayment(Payment payment)
        {
            _context.Update(payment);
            return Save();
        }

        public Payment GetPayment(int id)
        {
           return _context.Payment.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool PaymentExists(int id)
        {
            return _context.Payment.Any(p => p.Id == id);
        }
    }
}
