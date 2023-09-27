using FoodOrderingApi.Model;
using System.Collections.Generic;

namespace FoodOrderingApi.Interfaces
{
    public interface ICartRepository
    {
        /*
         NAME

            GetCart - Retrieve a cart by its unique identifier.

         SYNOPSIS

            Cart GetCart(int id)

         DESCRIPTION

            This function retrieves a cart from the database based on its unique identifier (id).

         PARAMETERS

            id - The unique identifier of the cart.

         RETURNS

            Returns the cart associated with the provided identifier.
        */
        Cart GetCart(int id);

        /*
         NAME

            GetCartByUserId - Retrieve a cart by the user's unique identifier.

         SYNOPSIS

            Cart GetCartByUserId(int userId)

         DESCRIPTION

            This function retrieves a cart from the database based on the user's unique identifier (userId).

         PARAMETERS

            userId - The unique identifier of the user.

         RETURNS

            Returns the cart associated with the provided user identifier.
        */
        Cart GetCartByUserId(int userId);

        /*
         NAME

            RemoveMenuItem - Remove a menu item from the cart.

         SYNOPSIS

            bool RemoveMenuItem(int menuItemId, Cart cart)

         DESCRIPTION

            This function removes a specific menu item from the cart.

         PARAMETERS

            menuItemId - The unique identifier of the menu item to be removed.
            cart - The cart from which to remove the menu item.

         RETURNS

            Returns true if the menu item was successfully removed from the cart; otherwise, false.
        */
        bool RemoveMenuItem(int menuItemId, Cart cart);

        /*
         NAME

            GetMenuItemsInCart - Retrieve menu items in a cart.

         SYNOPSIS

            ICollection<MenuItem> GetMenuItemsInCart(int cartId)

         DESCRIPTION

            This function retrieves a collection of menu items that are currently in the specified cart.

         PARAMETERS

            cartId - The unique identifier of the cart.

         RETURNS

            Returns a collection of menu items in the cart.
        */
        ICollection<MenuItem> GetMenuItemsInCart(int cartId);

        /*
         NAME

            AddCartItem - Add a cart item to the cart.

         SYNOPSIS

            bool AddCartItem(CartItem cartItem)

         DESCRIPTION

            This function adds a cart item to the cart.

         PARAMETERS

            cartItem - The cart item to be added to the cart.

         RETURNS

            Returns true if the cart item was successfully added to the cart; otherwise, false.
        */
        bool AddCartItem(CartItem cartItem);

        /*
         NAME

            AddMenuItemById - Add a menu item to the cart by its unique identifier.

         SYNOPSIS

            bool AddMenuItemById(int menuItemsId, Cart cart)

         DESCRIPTION

            This function adds a menu item to the cart based on its unique identifier (menuItemsId).

         PARAMETERS

            menuItemsId - The unique identifier of the menu item to be added to the cart.
            cart - The cart to which the menu item should be added.

         RETURNS

            Returns true if the menu item was successfully added to the cart; otherwise, false.
        */
        bool AddMenuItemById(int menuItemsId, Cart cart);

        /*
         NAME

            AddMenuItemByName - Add a menu item to the cart by its name.

         SYNOPSIS

            bool AddMenuItemByName(string menuItemsName, Cart cart)

         DESCRIPTION

            This function adds a menu item to the cart based on its name (menuItemsName).

         PARAMETERS

            menuItemsName - The name of the menu item to be added to the cart.
            cart - The cart to which the menu item should be added.

         RETURNS

            Returns true if the menu item was successfully added to the cart; otherwise, false.
        */
        bool AddMenuItemByName(string menuItemsName, Cart cart);

        /*
         NAME

            GetCartId - Get the unique identifier of a cart.

         SYNOPSIS

            int GetCartId(Cart cart)

         DESCRIPTION

            This function retrieves the unique identifier of a cart.

         PARAMETERS

            cart - The cart for which to retrieve the unique identifier.

         RETURNS

            Returns the unique identifier of the cart.
        */
        int GetCartId(Cart cart);

        /*
         NAME

            GetCartItemsInCart - Retrieve cart items in a cart.

         SYNOPSIS

            List<CartItem> GetCartItemsInCart(int cartId)

         DESCRIPTION

            This function retrieves a list of cart items that are currently in the specified cart.

         PARAMETERS

            cartId - The unique identifier of the cart.

         RETURNS

            Returns a list of cart items in the cart.
        */
        List<CartItem> GetCartItemsInCart(int cartId);

        /*
         NAME

            CartExists - Check if a cart exists.

         SYNOPSIS

            bool CartExists(int id)

         DESCRIPTION

            This function checks if a cart exists based on its unique identifier (id).

         PARAMETERS

            id - The unique identifier of the cart.

         RETURNS

            Returns true if the cart exists; otherwise, false.
        */
        bool CartExists(int id);

        /*
         NAME

            CreateCart - Create a new cart.

         SYNOPSIS

            bool CreateCart(Cart cart)

         DESCRIPTION

            This function creates a new cart.

         PARAMETERS

            cart - The cart to be created.

         RETURNS

            Returns true if the cart was successfully created; otherwise, false.
        */
        bool CreateCart(Cart cart);

        /*
         NAME

            UpdateCart - Update an existing cart.

         SYNOPSIS

            bool UpdateCart(Cart cart)

         DESCRIPTION

            This function updates an existing cart.

         PARAMETERS

            cart - The cart to be updated.

         RETURNS

            Returns true if the cart was successfully updated; otherwise, false.
        */
        bool UpdateCart(Cart cart);

        /*
         NAME

            DeleteCart - Delete a cart.

         SYNOPSIS

            bool DeleteCart(Cart cart)

         DESCRIPTION

            This function deletes a cart.

         PARAMETERS

            cart - The cart to be deleted.

         RETURNS

            Returns true if the cart was successfully deleted; otherwise, false.
        */
        bool DeleteCart(Cart cart);

        /*
         NAME

            ClearCart - Clear all items from a cart.

         SYNOPSIS

            bool ClearCart(Cart cart)

         DESCRIPTION

            This function removes all items from a cart.

         PARAMETERS

            cart - The cart from which to remove all items.

         RETURNS

            Returns true if the cart was successfully cleared; otherwise, false.
        */
        bool ClearCart(Cart cart);

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
