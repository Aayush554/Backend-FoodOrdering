using AutoMapper;
using FoodOrderingApi.Data;
using FoodOrderingApi.Dto;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private ICartRepository _cartRepository;
        private IUserRepository _userRepository;
        private readonly DataContext _context;

        private readonly IMapper _mapper;

        public OrderRepository(DataContext context, IMapper mapper, IUserRepository userRepository, ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _context = context;
            _mapper = mapper;
        }

                /*
         NAME

            CreateOrder - Creates a new order.

         DESCRIPTION

            This method creates a new order in the data context based on the items in the user's cart.

         PARAMETERS

            order - The Order object representing the order to be created.

         RETURNS

            Returns true if the order creation was successful; otherwise, false.
         */
        public bool CreateOrder(Order order)
        {
            int cartId = (int)_userRepository.GetCartIdByUserId((int)order.UserId);
            List<CartItem> cartItems = _context.CartItems.Where(c => c.CartId == cartId).ToList();
            double totalPrice = 0.0;
            foreach(CartItem cartItem in cartItems)
            {
                totalPrice += (double)cartItem.Price;
            }
            order.TotalPrice = totalPrice;
            _context.Orders.Add(order);
            if (!Save())
            {
                Console.WriteLine("order could not be saved");
                return false;
            }

            foreach (var items in cartItems)
            {
                OrderedItems orderedItem = new OrderedItems
                {
                    OrderId = order.Id,
                    MenuItemId = items.MenuItemId,
                    MenuItem = items.MenuItem,
                    Quantity = items.Quantity,
                    Price = items.Price
                };
                _context.OrderedItems.Add(orderedItem);
            }
            var cart = _context.Carts.Where(c => c.Id == cartId).FirstOrDefault();
            if (!_cartRepository.ClearCart(cart))
            {
                Console.WriteLine("something went worng while clearing the cart and cart items");
                return false;
            }
            return true;
        }

                /*
         NAME

            DeleteOrder - Deletes an order.

         DESCRIPTION

            This method removes an order from the data context.

         PARAMETERS

            order - The Order object representing the order to be deleted.

         RETURNS

            Returns true if the order deletion was successful; otherwise, false.
         */
        public bool DeleteOrder(Order order)
        {
            _context.Remove(order);
            return Save();
        }

                /*
         NAME

            GetAllOrders - Retrieves all orders.

         DESCRIPTION

            This method retrieves a collection of all orders from the data context.

         RETURNS

            Returns a collection of OrderedItems objects representing all orders.
         */
        public ICollection<OrderedItems> GetAllOrders()
        {
            return _context.OrderedItems.ToList();

        }


                /*
         NAME

            GetOrder - Retrieves an order by ID.

         DESCRIPTION

            This method retrieves an order with the specified ID from the data context.

         PARAMETERS

            id - An integer representing the ID of the order.

         RETURNS

            Returns the Order object representing the order.
         */
        public Order GetOrder(int id)
        {
            return _context.Orders.Where(o => o.Id == id).FirstOrDefault();
        }


                /*
         NAME

            GetOrdersById - Retrieves orders by user ID.

         DESCRIPTION

            This method retrieves a collection of orders associated with the specified user ID from the data context.

         PARAMETERS

            userId - An integer representing the ID of the user.

         RETURNS

            Returns a collection of OrderedItems objects representing the orders for the user.
         */
        public ICollection<OrderedItems> GetOrdersById(int userId)
        {
            var orders = _context.Orders.Where(o => o.User.Id == userId).ToList();
            List<OrderedItems> orderedItems = new List<OrderedItems>();
            foreach (Order order in orders)
            {
                var orderedItemlist = _context.OrderedItems.Where(o => o.OrderId == order.Id).ToList();
                orderedItems.AddRange(orderedItemlist);
            }
            return orderedItems;

        }
                /*
         NAME

            GetTotalPrice - Retrieves the total price of an order.

         DESCRIPTION

            This method retrieves the total price of an order with the specified ID from the data context.

         PARAMETERS

            orderId - An integer representing the ID of the order.

         RETURNS

            Returns the total price of the order as a decimal value.
         */
        public decimal GetTotalPrice(int orderId)
        {
            return (decimal)_context.Orders.Where(o => o.Id == orderId).FirstOrDefault().TotalPrice;
        }
        /*
         NAME

            OrderExists - Checks if an order with a specific ID exists.

         DESCRIPTION

            This method checks if an order with the specified ID exists in the data context.

         PARAMETERS

            id - An integer representing the ID of the order.

         RETURNS

            Returns true if an order with the specified ID exists; otherwise, false.
         */

        public bool OrderExists(int id)
        {
            return _context.Orders.Any(o => o.Id == id);
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

            UpdateOrder - Updates an order.

         DESCRIPTION

            This method updates an order in the data context.

         PARAMETERS

            order - The Order object representing the order to be updated.

         RETURNS

            Returns true if the update operation was successful; otherwise, false.
         */
        public bool UpdateOrder(Order order)
        {
            _context.Update(order);
            return Save();
        }
    }
}
