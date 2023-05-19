using AutoMapper;
using FoodOrderingApi.Data;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CreateOrder(Order order)
        {
            _context.Add(order);
            return Save();
        }

        public bool DeleteOrder(Order order)
        {
            _context.Remove(order);
            return Save();
        }

        public ICollection<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<Order> GetOrderByName(string userName)
        {
            int userId = (int)_context.Users.Where(u => u.UserName == userName).FirstOrDefault().Id ;
            return GetOrdersById(userId);
        }

        public ICollection<Order> GetOrdersById(int id)
        {
            return _context.Orders.Where(o => o.User.Id == id).ToList();

        }

        public decimal GetTotalPrice(int orderId)
        {
            throw new NotImplementedException();
        }

        public bool OrderExists(int id)
        {
            return _context.Orders.Any(o => o.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOrder(Order order)
        {
            _context.Update(order);
            return Save();
        }
    }
}
