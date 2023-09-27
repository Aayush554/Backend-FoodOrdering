using System;
using System.Reflection;
using AutoMapper;
using FoodOrderingApi.Data;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApi.Repository
{
	public class AdminRepository: IAdminRepository
	{
        private IMenuItemRepository _menuItemRepository;

        private readonly DataContext _context;

        public AdminRepository(DataContext context)
		{

            _context = context;
        }
        private string ErrorMessage(string methodName)
        {
            try
            {
               string errorMessage = "There was error in menthod" + methodName;
                return errorMessage;
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was error in menthod" + methodName + "\n" + ex.Message);
                return "Internal Server Error";
            }
        }
                /*
         NAME

            TotalCategory - Retrieves the total number of categories in the system.

         DESCRIPTION

            This method queries the data context to count the total number of categories
            available in the system and returns the count.

         RETURNS

            Returns an integer representing the total number of categories in the system.
         */
        public int TotalCategory()
        {
            try {

                int total = _context.Categories.ToList().Count;
                return total;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ErrorMessage("TotalCategory"));
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
                /*
         NAME

            TotalProducts - Retrieves the total number of products (menu items) in the system.

         DESCRIPTION

            This method queries the data context to count the total number of menu items
            available in the system and returns the count.

         RETURNS

            Returns an integer representing the total number of menu items in the system.
         */
        public int TotalProducts()
        {
            try
            {

                int total = _context.MenuItems.ToList().Count;
                return total;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ErrorMessage("TotalProducts"));
                Console.WriteLine(ex.Message);
                return 0;
            }
        }


        /*
         NAME

            TotalSalesByCategory - Calculates the total sales amount for a specific category.

         DESCRIPTION

            This method calculates the total sales amount for a specified category by
            iterating through ordered items and summing up the prices of items belonging
            to the category.

         PARAMETERS

            CategoryId - An integer representing the ID of the category.

         RETURNS

            Returns a double representing the total sales amount for the category.
         */
        public double TotalSalesByCategory(int CategoryId)
        {
            try
            {

                double totalPrice = 0.00;
                List<OrderedItems> orderedItems = _context.OrderedItems.ToList();
                
                
                foreach(var orderedItem in orderedItems)
                {
                    int menuItemId = (int)orderedItem.MenuItemId;
                    var menuItem = _context.MenuItems.Where(m => m.Id == menuItemId).FirstOrDefault();
                    if(menuItem.CategoryId == CategoryId)
                    {
                        totalPrice += (double)orderedItem.Price;
                    }
                }

                return totalPrice;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ErrorMessage("TotalProducts"));
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
                /*
         NAME

            TotalSalesByMonth - Calculates the total sales amount for a specific month.

         DESCRIPTION

            This method calculates the total sales amount for a specified month by querying
            the orders within the given month and summing up their total prices.

         PARAMETERS

            date - A DateOnly object representing the desired month and year.

         RETURNS

            Returns a double representing the total sales amount for the month.
         */
        public double TotalSalesByMonth(DateOnly date)
        {
            try
            {
                DateOnly firstDayOfMonth = new DateOnly(date.Year, date.Month, 1);
               
                DateOnly lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                double totalPrice = _context.Orders
                    .Where(o => o.OrderDate >= firstDayOfMonth.ToDateTime(TimeOnly.MinValue) && o.OrderDate <= lastDayOfMonth.ToDateTime(TimeOnly.MinValue))
                    .Sum(o => o.TotalPrice);

                return totalPrice;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ErrorMessage("TotalSalesByMonth"));
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

                /*
         NAME

            TotalUser - Retrieves the total number of users in the system.

         DESCRIPTION

            This method queries the data context to count the total number of users
            available in the system and returns the count.

         RETURNS

            Returns an integer representing the total number of users in the system.
         */
        public int TotalUser()
        {
            try
            {

                int total = _context.Users.ToList().Count;
                return total;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ErrorMessage("TotalProducts"));
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
                /*
         NAME

            CategoriesNames - Retrieves the names of all categories in the system.

         DESCRIPTION

            This method queries the data context to retrieve the names of all categories
            available in the system and returns a list of category names.

         RETURNS

            Returns a list of strings representing the names of categories in the system.
         */
        public List<string> CategoriesNames()
        {
            try
            {
                List<string> categoryName = new();
                List<Category> categories = _context.Categories.ToList();
                foreach(var category in categories)
                {
                    categoryName.Add(category.Name);
                }
                return categoryName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ErrorMessage("TotalProducts"));
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    
    }
}

