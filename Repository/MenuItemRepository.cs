using AutoMapper;
using FoodOrderingApi.Data;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApi.Repository
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MenuItemRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
                /*
         NAME

            CreateMenuItem - Creates a new menu item.

         DESCRIPTION

            This method adds a new menu item to the data context.

         PARAMETERS

            menuItem - The MenuItem object representing the menu item to be created.

         RETURNS

            Returns true if the menu item creation was successful; otherwise, false.
         */
        public bool CreateMenuItem(MenuItem menuItem)
        {
            _context.Add(menuItem);
            return Save();
        }

                /*
         NAME

            DeleteMenuItem - Deletes a menu item.

         DESCRIPTION

            This method removes a menu item from the data context.

         PARAMETERS

            menuItem - The MenuItem object representing the menu item to be deleted.

         RETURNS

            Returns true if the menu item deletion was successful; otherwise, false.
         */
        public bool DeleteMenuItem(MenuItem menuItem)
        {
            _context.Remove(menuItem);
            return Save();
        }
                /*
         NAME

            GetCategory - Retrieves the category of a menu item.

         DESCRIPTION

            This method retrieves the category of a menu item with the specified ID from the data context.

         PARAMETERS

            menuItemId - An integer representing the ID of the menu item.

         RETURNS

            Returns the Category object representing the category of the menu item.
         */
        public Category GetCategory(int menuItemId)
        {
            return _context.MenuItems.Where(m => m.Id == menuItemId)
                .Select(c => c.Category)
                .FirstOrDefault();
        }
                /*
         NAME

            GetMenuItem - Retrieves a menu item by ID.

         DESCRIPTION

            This method retrieves a menu item with the specified ID from the data context.

         PARAMETERS

            menuItemId - An integer representing the ID of the menu item.

         RETURNS

            Returns the MenuItem object representing the menu item.
         */
        public MenuItem GetMenuItem(int menuItemId)
        {
            return _context.MenuItems.Where(m => m.Id == menuItemId).FirstOrDefault();
        }
                /*
         NAME

            GetMenuItemByCategory - Retrieves menu items by category.

         DESCRIPTION

            This method retrieves a collection of menu items that belong to the specified category from the data context.

         PARAMETERS

            categoryId - An integer representing the ID of the category.

         RETURNS

            Returns a collection of MenuItem objects representing the menu items in the category.
         */
        public ICollection<MenuItem> GetMenuItemByCategory(int categoryId)
        {
            return _context.MenuItems.Where(m => m.CategoryId == categoryId).ToList();
        }
                /*
         NAME

            GetMenuItemByName - Retrieves a menu item by name.

         DESCRIPTION

            This method retrieves a menu item with the specified name from the data context.

         PARAMETERS

            menuItemName - A string representing the name of the menu item.

         RETURNS

            Returns the MenuItem object representing the menu item.
         */

        public MenuItem GetMenuItemByName(string menuItemName)
        {
            return _context.MenuItems.Where(m => m.Name == menuItemName).FirstOrDefault();
        }
                /*
         NAME

            GetMenuItems - Retrieves all menu items.

         DESCRIPTION

            This method retrieves a collection of all menu items from the data context.

         RETURNS

            Returns a collection of MenuItem objects representing all menu items.
         */
        public ICollection<MenuItem> GetMenuItems()
        {
            return _context.MenuItems.ToList();
        }
                /*
         NAME

            GetPrice - Retrieves the price of a menu item.

         DESCRIPTION

            This method retrieves the price of a menu item with the specified ID from the data context.

         PARAMETERS

            menuItemId - An integer representing the ID of the menu item.

         RETURNS

            Returns the price of the menu item as a decimal value.
         */
        public decimal GetPrice(int menuItemId)
        {
            return (decimal)_context.MenuItems.Where(m => m.Id == menuItemId).Select(m => m.Price).FirstOrDefault();
        }

                /*
         NAME

            IsAvailable - Checks if a menu item is available.

         DESCRIPTION

            This method checks if a menu item with the specified ID is available in the data context.

         PARAMETERS

            menuItemId - An integer representing the ID of the menu item.

         RETURNS

            Returns true if the menu item with the specified ID is available; otherwise, false.
         */
        public bool IsAvailable(int menuItemId)
        {
            return _context.MenuItems.Any(m => m.Id == menuItemId);
        }
                /*
         NAME

            MenuItemExists - Checks if a menu item with a specific ID exists.

         DESCRIPTION

            This method checks if a menu item with the specified ID exists in the data context.

         PARAMETERS

            menuItemId - An integer representing the ID of the menu item.

         RETURNS

            Returns true if a menu item with the specified ID exists; otherwise, false.
         */

        public bool MenuItemExists(int menuItemId)
        {
            return _context.MenuItems.Any(m => m.Id == menuItemId);
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

            UpdateMenuItem - Updates a menu item.

         DESCRIPTION

            This method updates a menu item in the data context.

         PARAMETERS

            menuItem - The MenuItem object representing the menu item to be updated.

         RETURNS

            Returns true if the update operation was successful; otherwise, false.
         */
        public bool UpdateMenuItem(MenuItem menuItem)
        {
            _context.Update(menuItem);
            return Save();
        }
    }
}
