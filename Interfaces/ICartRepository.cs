using FoodOrderingApi.Model;

namespace FoodOrderingApi.Interfaces
{
    public interface ICartRepository
    {
        Cart GetCart(int id);
        Cart GetCartByUserId(int userId);
        bool RemoveMenuItem(int menuItemId, Cart cart);
        ICollection<MenuItem> GetMenuItemsInCart(int cartId);
        bool AddMenuItemById(int menuItemsId, Cart cart);
        bool AddMenuItemByName(string menuItemsName, Cart cart);

        bool CartExists(int id);
        bool CreateCart(Cart cart);
        bool UpdateCart(Cart cart);
        bool DeleteCart(Cart cart);
        bool Save();
    }
}
