using AutoMapper;
using FoodOrderingApi.Data;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;

namespace FoodOrderingApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<Order>? GetOrdersById(int userId)
        {
            return (ICollection<Order>)_context.Users.Where(p => p.Id == userId).Select(o => o.Orders).ToList();
        }

        public ICollection<Payment>? GetPaymentsById(int userId)
        {
            return (ICollection<Payment>)_context.Users.Where(p => p.Id == userId).Select(o => o.Payments).ToList();

        }

        public User? GetUser(int userId)
        {
            return _context.Users.Where(p => p.Id == userId).FirstOrDefault();
        }

        public int? GetUserCount()
        {
            return _context.Users.Count();
        }

        public ICollection<User>? GetUsers()
        {
            return _context.Users.ToList(); 
        }

        public bool? UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public int? GetCartIdByUserId(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).Select(c => c.CartId).FirstOrDefault();
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public User? GetUserByName(string userName)
        {
            return _context.Users.Where(u =>u.UserName == userName).FirstOrDefault();
        }
    }
}
