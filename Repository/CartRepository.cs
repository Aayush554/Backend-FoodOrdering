using AutoMapper;
using FoodOrderingApi.Data;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;

namespace FoodOrderingApi.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CartRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AddMenuItemById(int menuItemsId, Cart cart)
        {
            var menuItem = _context.MenuItems.Where(c => c.Id == menuItemsId).FirstOrDefault();
            var cartItem = new CartItem()
            {
                MenuItem = menuItem,
                Cart = cart,
            };
            _context.Add(cartItem);
            return Save();

        }
        public bool AddMenuItemByName(string menuItemsName, Cart cart)
        {
            var menuItem = _context.MenuItems.Where(c => c.Name == menuItemsName).FirstOrDefault();
            var cartItem = new CartItem()
            {
                MenuItem = menuItem,
                Cart = cart,
            };
            _context.Add(cartItem);
            return Save();

        }
        public bool CartExists(int id)
        {
            return _context.Carts.Any(c => c.Id == id);
        }

        public bool CreateCart(Cart cart)
        {
            _context.Add(cart);
            return Save();
        }

        public bool DeleteCart(Cart cart)
        {
            _context.Remove(cart);
            return Save();
        }

        public Cart GetCart(int id)
        {
            return _context.Carts.Where(c => c.Id == id).FirstOrDefault();
        }

        public Cart? GetCartByUserId(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).Select(c => c.Cart).FirstOrDefault();
        }

        public ICollection<MenuItem> GetMenuItemsInCart(int cartId)
        {
            return _context.CartItems.Where(c => c.CartId == cartId).Select(m => m.MenuItem).ToList();    
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCart(Cart cart)
        {
            _context.Update(cart);
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
    }
}
