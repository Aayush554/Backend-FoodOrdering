using FoodOrderingApi.Dto;
using FoodOrderingApi.Model;
using System;
using System.Collections.Generic;

namespace FoodOrderingApi.Interfaces
{
    public interface IOrderRepository
    {
        /*
         NAME

            GetOrder - Retrieve an order by ID.

         SYNOPSIS

            Order GetOrder(int id)

         DESCRIPTION

            This function retrieves an order by its unique identifier.

         PARAMETERS

            id - The unique identifier of the order.

         RETURNS

            Returns the order associated with the provided ID.
        */
        Order GetOrder(int id);

        /*
         NAME

            GetOrdersById - Retrieve orders by user ID.

         SYNOPSIS

            ICollection<OrderedItems> GetOrdersById(int userId)

         DESCRIPTION

            This function retrieves a collection of orders based on the user's ID.

         PARAMETERS

            userId - The ID of the user.

         RETURNS

            Returns a collection of orders associated with the provided user ID.
        */
        ICollection<OrderedItems> GetOrdersById(int userId);

        /*
         NAME

            GetAllOrders - Retrieve all orders.

         SYNOPSIS

            ICollection<OrderedItems> GetAllOrders()

         DESCRIPTION

            This function retrieves a collection of all orders.

         RETURNS

            Returns a collection of all orders.
        */
        ICollection<OrderedItems> GetAllOrders();

        /*
         NAME

            GetTotalPrice - Retrieve the total price of an order.

         SYNOPSIS

            decimal GetTotalPrice(int orderId)

         DESCRIPTION

            This function retrieves the total price of an order by its unique identifier.

         PARAMETERS

            orderId - The unique identifier of the order.

         RETURNS

            Returns the total price of the order.
        */
        decimal GetTotalPrice(int orderId);

        /*
         NAME

            OrderExists - Check if an order exists.

         SYNOPSIS

            bool OrderExists(int id)

         DESCRIPTION

            This function checks if an order exists based on its unique identifier.

         PARAMETERS

            id - The unique identifier of the order.

         RETURNS

            Returns true if the order exists; otherwise, false.
        */
        bool OrderExists(int id);

        /*
         NAME

            CreateOrder - Create a new order.

         SYNOPSIS

            bool CreateOrder(Order order)

         DESCRIPTION

            This function creates a new order.

         PARAMETERS

            order - The order to be created.

         RETURNS

            Returns true if the order was successfully created; otherwise, false.
        */
        bool CreateOrder(Order order);

        /*
         NAME

            UpdateOrder - Update an existing order.

         SYNOPSIS

            bool UpdateOrder(Order order)

         DESCRIPTION

            This function updates an existing order.

         PARAMETERS

            order - The order to be updated.

         RETURNS

            Returns true if the order was successfully updated; otherwise, false.
        */
        bool UpdateOrder(Order order);

        /*
         NAME

            DeleteOrder - Delete an order.

         SYNOPSIS

            bool DeleteOrder(Order order)

         DESCRIPTION

            This function deletes an order.

         PARAMETERS

            order - The order to be deleted.

         RETURNS

            Returns true if the order was successfully deleted; otherwise, false.
        */
        bool DeleteOrder(Order order);

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
