using FoodOrderingApi.Model;
using System.Reflection.Metadata.Ecma335;

namespace FoodOrderingApi.Interfaces
{
    public interface IUserRepository
    {
        /*
         NAME

            GetUsers - Retrieve all users.

         SYNOPSIS

            ICollection<User>? GetUsers()

         DESCRIPTION

            This function retrieves all users available.

         RETURNS

            Returns a collection of all users.
        */
        ICollection<User>? GetUsers();

        /*
         NAME

            GetUser - Retrieve a user by ID.

         SYNOPSIS

            User? GetUser(int userId)

         DESCRIPTION

            This function retrieves a user by their unique identifier.

         PARAMETERS

            userId - The unique identifier of the user.

         RETURNS

            Returns the user associated with the provided ID.
        */
        User? GetUser(int userId);

        /*
         NAME

            GetUserByName - Retrieve a user by name.

         SYNOPSIS

            User? GetUserByName(string userName)

         DESCRIPTION

            This function retrieves a user by their name.

         PARAMETERS

            userName - The name of the user to retrieve.

         RETURNS

            Returns the user associated with the provided name.
        */
        User? GetUserByName(string userName);

        /*
         NAME

            GetUserByEmail - Retrieve a user by email.

         SYNOPSIS

            User? GetUserByEmail(string userEmail)

         DESCRIPTION

            This function retrieves a user by their email.

         PARAMETERS

            userEmail - The email of the user to retrieve.

         RETURNS

            Returns the user associated with the provided email.
        */
        User? GetUserByEmail(string userEmail);

        /*
         NAME

            GetUserCount - Get the total number of users.

         SYNOPSIS

            int? GetUserCount()

         DESCRIPTION

            This function returns the total number of users.

         RETURNS

            Returns the total number of users or null if the count is not available.
        */
        int? GetUserCount();

        /*
         NAME

            UserExists - Check if a user exists.

         SYNOPSIS

            bool? UserExists(int userId)

         DESCRIPTION

            This function checks if a user exists based on their unique identifier.

         PARAMETERS

            userId - The unique identifier of the user.

         RETURNS

            Returns true if the user exists; otherwise, false or null if the existence status is not available.
        */
        bool? UserExists(int userId);

        /*
         NAME

            GetOrdersById - Retrieve orders by user ID.

         SYNOPSIS

            ICollection<Order>? GetOrdersById(int userId)

         DESCRIPTION

            This function retrieves orders associated with a user based on their ID.

         PARAMETERS

            userId - The ID of the user whose orders are to be retrieved.

         RETURNS

            Returns a collection of orders associated with the provided user ID or null if no orders are found.
        */
        ICollection<Order>? GetOrdersById(int userId);

        /*
         NAME

            GetPaymentsById - Retrieve payments by user ID.

         SYNOPSIS

            ICollection<Payment>? GetPaymentsById(int userId)

         DESCRIPTION

            This function retrieves payments associated with a user based on their ID.

         PARAMETERS

            userId - The ID of the user whose payments are to be retrieved.

         RETURNS

            Returns a collection of payments associated with the provided user ID or null if no payments are found.
        */
        ICollection<Payment>? GetPaymentsById(int userId);

        /*
         NAME

            GetCartIdByUserId - Retrieve cart ID by user ID.

         SYNOPSIS

            int? GetCartIdByUserId(int userId)

         DESCRIPTION

            This function retrieves the cart ID associated with a user based on their ID.

         PARAMETERS

            userId - The ID of the user whose cart ID is to be retrieved.

         RETURNS

            Returns the cart ID associated with the provided user ID or null if no cart is found.
        */
        int? GetCartIdByUserId(int userId);

        // CRUD operations

        /*
         NAME

            CreateUser - Create a new user.

         SYNOPSIS

            bool CreateUser(User user)

         DESCRIPTION

            This function creates a new user.

         PARAMETERS

            user - The user to be created.

         RETURNS

            Returns true if the user was successfully created; otherwise, false.
        */
        bool CreateUser(User user);

        /*
         NAME

            UpdateUser - Update an existing user.

         SYNOPSIS

            bool UpdateUser(User user)

         DESCRIPTION

            This function updates an existing user.

         PARAMETERS

            user - The user to be updated.

         RETURNS

            Returns true if the user was successfully updated; otherwise, false.
        */
        bool UpdateUser(User user);

        /*
         NAME

            DeleteUser - Delete a user.

         SYNOPSIS

            bool DeleteUser(User user)

         DESCRIPTION

            This function deletes a user.

         PARAMETERS

            user - The user to be deleted.

         RETURNS

            Returns true if the user was successfully deleted; otherwise, false.
        */
        bool DeleteUser(User user);

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
