using FoodOrderingApi.Model;
using System;
using System.Collections.Generic;

namespace FoodOrderingApi.Interfaces
{
    public interface IMenuItemRepository
    {
        /*
         NAME

            GetMenuItems - Retrieve all menu items.

         SYNOPSIS

            ICollection<MenuItem> GetMenuItems()

         DESCRIPTION

            This function retrieves a collection of all menu items.

         RETURNS

            Returns a collection of all menu items.
        */
        ICollection<MenuItem> GetMenuItems();

        /*
         NAME

            GetMenuItem - Retrieve a menu item by ID.

         SYNOPSIS

            MenuItem GetMenuItem(int menuItemId)

         DESCRIPTION

            This function retrieves a menu item by its unique identifier.

         PARAMETERS

            menuItemId - The unique identifier of the menu item.

         RETURNS

            Returns the menu item associated with the provided ID.
        */
        MenuItem GetMenuItem(int menuItemId);

        /*
         NAME

            GetMenuItemByCategory - Retrieve menu items by category.

         SYNOPSIS

            ICollection<MenuItem> GetMenuItemByCategory(int categoryId)

         DESCRIPTION

            This function retrieves a collection of menu items based on their category.

         PARAMETERS

            categoryId - The ID of the category.

         RETURNS

            Returns a collection of menu items in the specified category.
        */
        ICollection<MenuItem> GetMenuItemByCategory(int categoryId);

        /*
         NAME

            GetMenuItemByName - Retrieve a menu item by name.

         SYNOPSIS

            MenuItem GetMenuItemByName(string menuItemName)

         DESCRIPTION

            This function retrieves a menu item by its name.

         PARAMETERS

            menuItemName - The name of the menu item.

         RETURNS

            Returns the menu item associated with the provided name.
        */
        MenuItem GetMenuItemByName(string menuItemName);

        /*
         NAME

            GetPrice - Retrieve the price of a menu item.

         SYNOPSIS

            decimal GetPrice(int menuItemId)

         DESCRIPTION

            This function retrieves the price of a menu item by its unique identifier.

         PARAMETERS

            menuItemId - The unique identifier of the menu item.

         RETURNS

            Returns the price of the menu item.
        */
        decimal GetPrice(int menuItemId);

        /*
         NAME

            IsAvailable - Check if a menu item is available.

         SYNOPSIS

            bool IsAvailable(int menuItemId)

         DESCRIPTION

            This function checks if a menu item is available for ordering.

         PARAMETERS

            menuItemId - The unique identifier of the menu item.

         RETURNS

            Returns true if the menu item is available; otherwise, false.
        */
        bool IsAvailable(int menuItemId);

        /*
         NAME

            MenuItemExists - Check if a menu item exists.

         SYNOPSIS

            bool MenuItemExists(int menuItemId)

         DESCRIPTION

            This function checks if a menu item exists based on its unique identifier.

         PARAMETERS

            menuItemId - The unique identifier of the menu item.

         RETURNS

            Returns true if the menu item exists; otherwise, false.
        */
        bool MenuItemExists(int menuItemId);

        /*
         NAME

            GetCategory - Retrieve the category of a menu item.

         SYNOPSIS

            Category GetCategory(int id)

         DESCRIPTION

            This function retrieves the category of a menu item by its unique identifier.

         PARAMETERS

            id - The unique identifier of the menu item.

         RETURNS

            Returns the category associated with the provided menu item.
        */
        Category GetCategory(int id);

        /*
         NAME

            CreateMenuItem - Create a new menu item.

         SYNOPSIS

            bool CreateMenuItem(MenuItem menuItem)

         DESCRIPTION

            This function creates a new menu item.

         PARAMETERS

            menuItem - The menu item to be created.

         RETURNS

            Returns true if the menu item was successfully created; otherwise, false.
        */
        bool CreateMenuItem(MenuItem menuItem);

        /*
         NAME

            DeleteMenuItem - Delete a menu item.

         SYNOPSIS

            bool DeleteMenuItem(MenuItem menuItem)

         DESCRIPTION

            This function deletes a menu item.

         PARAMETERS

            menuItem - The menu item to be deleted.

         RETURNS

            Returns true if the menu item was successfully deleted; otherwise, false.
        */
        bool DeleteMenuItem(MenuItem menuItem);

        /*
         NAME

            UpdateMenuItem - Update an existing menu item.

         SYNOPSIS

            bool UpdateMenuItem(MenuItem menuItem)

         DESCRIPTION

            This function updates an existing menu item.

         PARAMETERS

            menuItem - The menu item to be updated.

         RETURNS

            Returns true if the menu item was successfully updated; otherwise, false.
        */
        bool UpdateMenuItem(MenuItem menuItem);

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
