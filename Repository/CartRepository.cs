using AutoMapper;
using FoodOrderingApi.Data;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApi.Repository
{
    public class CartRepository : ICartRepository
    {
        private IMenuItemRepository _menuItemRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CartRepository(DataContext context, IMapper mapper, IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
            _context = context;
            _mapper = mapper;
        }

                /*
         NAME

            AddMenuItemById - Adds a menu item to the cart by its ID.

         DESCRIPTION

            This method adds a menu item to the specified cart by its ID. If the menu item
            is already in the cart, it increments the quantity; otherwise, it creates a new
            cart item.

         PARAMETERS

            menuItemsId - An integer representing the ID of the menu item to add.
            cart - The cart to which the menu item is added.

         RETURNS

            Returns true if the operation was successful; otherwise, false.
         */
        public bool AddMenuItemById(int menuItemsId, Cart cart)
        {
            var menuItem = _context.MenuItems.SingleOrDefault(c => c.Id == menuItemsId);

            if (menuItem == null)
            {
                return false;
            }

            // Check if the menu item already exists in the cart
            var existingCartItem = _context.CartItems.Where(c => c.CartId == cart.Id).Where(m => m.MenuItemId == menuItemsId).FirstOrDefault();
            if (existingCartItem != null)
            {
                _context.Remove(existingCartItem);
                if (Save())
                {
                    var newCartItem = new CartItem()
                    {
                        MenuItemId = menuItem.Id,
                        MenuItem = menuItem,
                        CartId = cart.Id,
                        Quantity = ++existingCartItem.Quantity,
                        Price = existingCartItem.Quantity * menuItem.Price
                    };
                    _context.CartItems.Add(newCartItem);
                }

                return Save();
            }
            var cartItem = new CartItem()
            {
                MenuItemId = menuItem.Id,
                MenuItem = menuItem,
                CartId = cart.Id,
                Quantity = 1,
                Price = menuItem.Price
            };
            var updatedCart = _context.Carts
                .Include(c => c.CartItems) // Include CartItems
                .SingleOrDefault(c => c.Id == cart.Id);

            if (updatedCart == null)
            {
                return false; // Handle case where cart is not found
            }

            updatedCart.CartItems.Add(cartItem); // Add the new CartItem to the Cart's list of CartItems
            if (!Save())
            {
                Console.WriteLine("could not save the cartItem to the cart");
                return false;
            }
            return true;
        }

                /*
         NAME

            AddMenuItemByName - Adds a menu item to the cart by its name.

         DESCRIPTION

            This method adds a menu item to the specified cart by its name. If the menu item
            is already in the cart, it increments the quantity; otherwise, it creates a new
            cart item.

         PARAMETERS

            menuItemsName - A string representing the name of the menu item to add.
            cart - The cart to which the menu item is added.

         RETURNS

            Returns true if the operation was successful; otherwise, false.
         */
        public bool AddMenuItemByName(string menuItemsName, Cart cart)
        {
            var menuItem = _context.MenuItems.SingleOrDefault(c => c.Name == menuItemsName);

            if (menuItem == null)
            {
                return false;
            }

            // Check if the menu item already exists in the cart
            var cartMenuItem = GetCartItemsInCart((int)cart.Id).Where(c => c.MenuItemId == menuItem.Id).FirstOrDefault();
            if (cartMenuItem != null)
            {
                cart.CartItems.SingleOrDefault(c => c.MenuItem.Name == menuItemsName).Quantity++;
                return UpdateCart(cart);
            }

            var cartItem = new CartItem()
            {
                MenuItemId = menuItem.Id,
                MenuItem = menuItem,
                CartId = cart.Id,
                Cart = GetCart((int)cart.Id),
                Quantity = 1,
                Price = menuItem.Price
            };
            _context.CartItems.Add(cartItem);

            // Save changes to generate an ID for the new CartItem
            _context.SaveChanges();

            // Update the cart's CartItems collection with the newly added CartItem
            cart.CartItems.Add(cartItem);

            // Save changes to the cart
            _context.Carts.Update(cart);
            if (!Save())
            {
                Console.WriteLine("could not save the cartItem to the cart");
                return false;
            }
            return true;

        }

                /*
         NAME

            CartExists - Checks if a cart with the specified ID exists.

         DESCRIPTION

            This method checks if a cart with the specified ID exists in the data context.

         PARAMETERS

            id - An integer representing the ID of the cart to check.

         RETURNS

            Returns true if the cart exists; otherwise, false.
         */
        public bool CartExists(int id)
        {
            return _context.Carts.Any(c => c.Id == id);
        }

        /*
         NAME

            CreateCart - Creates a new cart.

         DESCRIPTION

            This method adds a new cart to the data context.

         PARAMETERS

            cart - The cart to be created.

         RETURNS

            Returns true if the operation was successful; otherwise, false.
         */
        public bool CreateCart(Cart cart)
        {
            try
            {
                _context.Add(cart);
                return Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Internal Server Error");
                return false;
            }
        }

        /*
         NAME

            DeleteCart - Deletes a cart.

         DESCRIPTION

            This method removes a cart from the data context.

         PARAMETERS

            cart - The cart to be deleted.

         RETURNS

            Returns true if the operation was successful; otherwise, false.
         */
        public bool DeleteCart(Cart cart)
        {
            try
            {
                _context.Remove(cart);
                return Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Internal Server Error");
                return false;
            }
        }

        /*
         NAME

            GetCart - Retrieves a cart by ID.

         DESCRIPTION

            This method retrieves a cart from the data context by its ID.

         PARAMETERS

            id - An integer representing the ID of the cart to retrieve.

         RETURNS

            Returns a Cart object if found; otherwise, null.
         */
        public Cart GetCart(int id)
        {
            try
            {
                return _context.Carts.Where(c => c.Id == id).FirstOrDefault();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Internal Server Error");
                return null;
            }
        }

        /*
         NAME

            GetCartByUserId - Retrieves a cart by user ID.

         DESCRIPTION

            This method retrieves a cart associated with a user by the user's ID.

         PARAMETERS

            userId - An integer representing the ID of the user.

         RETURNS

            Returns a Cart object if found; otherwise, null.
         */
        public Cart? GetCartByUserId(int userId)
        {
            try
            {
                return _context.Users.Where(u => u.Id == userId).Select(c => c.Cart).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Internal Server Error");
                return null;
            }
        }
        /*
 NAME

    GetCartItemsInCart - Retrieves cart items in a cart.

 DESCRIPTION

    This method retrieves a list of cart items in the specified cart.

 PARAMETERS

    cartId - An integer representing the ID of the cart.

 RETURNS

    Returns a list of CartItem objects in the cart.
 */
        public List<CartItem> GetCartItemsInCart(int cartId)
        {
            var cart = _context.Carts.SingleOrDefault(c => c.Id == cartId);
            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
            {
                return new List<CartItem>();
            }

            return cart.CartItems.ToList();
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

        public bool UpdateCart(Cart cart)
        {
            _context.Carts.Update(cart);
            return Save();
        }
        public bool RemoveMenuItem(int menuItemId, Cart cart)
        {
            var cartItem = _context.CartItems.Where(c => c.MenuItemId == menuItemId && c.CartId == cart.Id).FirstOrDefault();

            if (cartItem != null)
            {
                _context.Remove(cartItem);
                return Save();
            }

            return false;
        }

                /*
         NAME

            GetCartId - Retrieves the ID of a cart.

         DESCRIPTION

            This method retrieves the ID of a cart associated with the provided cart object.

         PARAMETERS

            cart - The Cart object for which to retrieve the ID.

         RETURNS

            Returns an integer representing the ID of the cart, or -999 if not found.
         */

        public int GetCartId(Cart cart)
        {
            try{
                int? cartId = _context.Carts.ToList().Where(c => c.UserId == cart.UserId).FirstOrDefault().Id;
                if (cartId == null)
                {
                    return -1;
                }
                return (int)cartId;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Internal Server Error");
                return -1;
            }
        }

                /*
         NAME

            GetMenuItemsInCart - Retrieves menu items in a cart.

         DESCRIPTION

            This method retrieves a list of menu items in the specified cart.

         PARAMETERS

            cartId - An integer representing the ID of the cart.

         RETURNS

            Returns a collection of MenuItem objects in the cart.
         */
        public ICollection<MenuItem> GetMenuItemsInCart(int cartId)
        {
            Cart cart = _context.Carts.SingleOrDefault(c => c.Id == cartId);
            var cartItems = _context.CartItems.Where(c => c.CartId == cartId).ToList();
            List<MenuItem> menuItems = new();
            if (cartItems.Count != 0)
            {
                foreach(var cartItem in cartItems) {
                    menuItems.Add(_context.MenuItems.SingleOrDefault(m=> m.Id == cartItem.MenuItemId));
                }

            }
            return menuItems;

        }
                /*
         NAME

            AddCartItem - Adds a cart item to a cart.

         DESCRIPTION

            This method adds a cart item to the specified cart and updates the cart's CartItems collection.

         PARAMETERS

            cartItem - The CartItem object to be added to the cart.

         RETURNS

            Returns true if the addition was successful; otherwise, false.
         */
        public bool AddCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
            Cart cart = _context.Carts.SingleOrDefault(c => c.Id == cartItem.CartId);
            // Update the cart's CartItems collection with the newly added CartItem
            cart.CartItems.Add(cartItem);

            return UpdateCart(cart);
        }
                /*
         NAME

            ClearCart - Clears all cart items from a cart.

         DESCRIPTION

            This method removes all cart items associated with the specified cart.

         PARAMETERS

            cart - The Cart object from which to remove all cart items.

         RETURNS

            Returns true if the clearing operation was successful; otherwise, false.
         */
        public bool ClearCart(Cart cart)
        {
            foreach(CartItem cartItem in _context.CartItems.Where(c=> c.CartId == cart.Id).ToList())
            {
                _context.CartItems.Remove(cartItem);
            }
            return Save();
        }
    }
}
