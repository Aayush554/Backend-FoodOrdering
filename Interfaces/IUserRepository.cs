using FoodOrderingApi.Model;
using System.Reflection.Metadata.Ecma335;

namespace FoodOrderingApi.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User>? GetUsers();

        User? GetUser(int userId);

        User? GetUserByName(string userName);
        int? GetUserCount();

        bool? UserExists(int userId);

        ICollection<Order>? GetOrdersById(int userId);

        ICollection<Payment>? GetPaymentsById(int userId);

        int? GetCartIdByUserId(int userId);

        //CRUD
        bool CreateUser (User user);
        bool UpdateUser (User user);
        bool DeleteUser (User user);
        bool Save();
    }
}
