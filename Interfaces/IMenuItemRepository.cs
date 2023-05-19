using FoodOrderingApi.Model;

namespace FoodOrderingApi.Interfaces
{
    public interface IMenuItemRepository
    {
        ICollection<MenuItem> GetMenuItems();
        MenuItem GetMenuItem(int menuItemId);
        ICollection<MenuItem> GetMenuItemByCategory(int categoryId);
        MenuItem GetMenuItemByName(string menuItemName);
        decimal GetPrice(int menuItemId);

        bool IsAvailable(int menuItemId);
        bool MenuItemExists(int menuItemId);
        Category GetCategory(int id);

        bool CreateMenuItem(MenuItem menuItem);
        bool DeleteMenuItem(MenuItem menuItem);
        bool UpdateMenuItem(MenuItem menuItem);
        bool Save();

    }
}
