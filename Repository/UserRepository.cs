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
                /*
         NAME

            GetOrdersById - Retrieves orders by user ID.

         DESCRIPTION

            This method retrieves a collection of Order objects associated with the specified user ID from the data context.

         PARAMETERS

            userId - An integer representing the ID of the user.

         RETURNS

            Returns a collection of Order objects representing the orders created by the user.
         */

        public ICollection<Order>? GetOrdersById(int userId)
        {
            return (ICollection<Order>)_context.Users.Where(p => p.Id == userId).Select(o => o.Orders).ToList();
        }
                /*
         NAME

            GetPaymentsById - Retrieves payments by user ID.

         DESCRIPTION

            This method retrieves a collection of Payment objects associated with the specified user ID from the data context.

         PARAMETERS

            userId - An integer representing the ID of the user.

         RETURNS

            Returns a collection of Payment objects representing the payments made by the user.
         */

        public ICollection<Payment>? GetPaymentsById(int userId)
        {
            return (ICollection<Payment>)_context.Users.Where(p => p.Id == userId).Select(o => o.Payments).ToList();

        }

                /*
         NAME

            GetUser - Retrieves a user by user ID.

         DESCRIPTION

            This method retrieves a User object by the specified user ID from the data context.

         PARAMETERS

            userId - An integer representing the ID of the user.

         RETURNS

            Returns a User object representing the user with the specified ID.
         */
        public User? GetUser(int userId)
        {
            return _context.Users.Where(p => p.Id == userId).FirstOrDefault();
        }

                /*
         NAME

            GetUserCount - Retrieves the total number of users.

         DESCRIPTION

            This method queries the data context to count the total number of users and returns the count.

         RETURNS

            Returns an integer representing the total number of users in the system.
         */
        public int? GetUserCount()
        {
            return _context.Users.Count();
        }


                /*
         NAME

            GetUsers - Retrieves all users.

         DESCRIPTION

            This method retrieves all User objects from the data context.

         RETURNS

            Returns a collection of User objects representing all users in the system.
         */

        public ICollection<User>? GetUsers()
        {
            return _context.Users.ToList(); 
        }

                /*
         NAME

            UserExists - Checks if a user exists by user ID.

         DESCRIPTION

            This method checks if a user with the specified user ID exists in the data context.

         PARAMETERS

            userId - An integer representing the ID of the user.

         RETURNS

            Returns true if the user exists; otherwise, false.
         */
        public bool? UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }

                /*
         NAME

            Save - Saves changes to the data context.

         DESCRIPTION

            This method saves changes made to the data context.

         RETURNS

            Returns true if the changes were successfully saved; otherwise, false.
         */

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

                /*
         NAME

            GetCartIdByUserId - Retrieves the cart ID by user ID.

         DESCRIPTION

            This method retrieves the cart ID associated with the specified user ID from the data context.

         PARAMETERS

            userId - An integer representing the ID of the user.

         RETURNS

            Returns an integer representing the cart ID of the user.
         */
        public int? GetCartIdByUserId(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).Select(c => c.CartId).FirstOrDefault();
        }

                /*
         NAME

            CreateUser - Creates a new user.

         DESCRIPTION

            This method adds a new User object to the data context.

         PARAMETERS

            user - The User object representing the new user to be created.

         RETURNS

            Returns true if the user creation was successful; otherwise, false.
         */
        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }


        /*
         NAME

            UpdateUser - Updates a user.

         DESCRIPTION

            This method updates a user in the data context.

         PARAMETERS

            user - The User object representing the updated user.

         RETURNS

            Returns true if the user update was successful; otherwise, false.
         */
        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

                /*
         NAME

            DeleteUser - Deletes a user.

         DESCRIPTION

            This method removes a User object from the data context.

         PARAMETERS

            user - The User object representing the user to be deleted.

         RETURNS

            Returns true if the user deletion was successful; otherwise, false.
         */
        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }
                /*
         NAME

            GetUserByName - Retrieves a user by user name.

         DESCRIPTION

            This method retrieves a User object by the specified user name from the data context.

         PARAMETERS

            userName - A string representing the user name.

         RETURNS

            Returns a User object representing the user with the specified name.
         */
        public User? GetUserByName(string userName)
        {
            return _context.Users.Where(u =>u.UserName == userName).FirstOrDefault();
        }
        /*
        NAME

           GetUserByEmail - Retrieves a user by email.

        DESCRIPTION

           This method retrieves a User object by the specified email address from the data context.

        PARAMETERS

           userEmail - A string representing the user's email address.

        RETURNS

           Returns a User object representing the user with the specified email address.
        */
        public User? GetUserByEmail(string userEmail)
        {
            return _context.Users.Where(u => u.Email == userEmail).FirstOrDefault();

        }
    }
}
