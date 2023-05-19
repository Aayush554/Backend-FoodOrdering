using FoodOrderingApi.Model;

namespace FoodOrderingApi.Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrder(int id);
        ICollection<Order> GetOrdersById(int id);
        ICollection<Order> GetOrderByName(string userName);
        ICollection<Order> GetAllOrders();
        decimal GetTotalPrice(int orderId);

        bool OrderExists(int id);

        bool CreateOrder (Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);

        bool Save();




    }
}
