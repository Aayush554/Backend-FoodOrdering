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
        public bool CreateMenuItem(MenuItem menuItem)
        {
            _context.Add(menuItem);
            return Save();
        }

        public bool DeleteMenuItem(MenuItem menuItem)
        {
            _context.Remove(menuItem);
            return Save();
        }

        public Category GetCategory(int menuItemId)
        {
            return _context.MenuItems.Where(m => m.Id == menuItemId)
                .Select(c => c.Category)
                .FirstOrDefault();
        }

        public MenuItem GetMenuItem(int menuItemId)
        {
            return _context.MenuItems.Where(m => m.Id == menuItemId).FirstOrDefault();
        }

        public ICollection<MenuItem> GetMenuItemByCategory(int categoryId)
        {
            return _context.MenuItems.Where(m => m.CategoryId == categoryId).ToList();
        }

        public MenuItem GetMenuItemByName(string menuItemName)
        {
            return _context.MenuItems.Where(m => m.Name == menuItemName).FirstOrDefault();
        }

        public ICollection<MenuItem> GetMenuItems()
        {
            return _context.MenuItems.ToList();
        }

        public decimal GetPrice(int menuItemId)
        {
            return (decimal)_context.MenuItems.Where(m => m.Id == menuItemId).Select(m => m.Price).FirstOrDefault();
        }

        public bool IsAvailable(int menuItemId)
        {
            return _context.MenuItems.Any(m => m.Id == menuItemId);
        }

        public bool MenuItemExists(int menuItemId)
        {
            return _context.MenuItems.Any(m => m.Id == menuItemId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMenuItem(MenuItem menuItem)
        {
            _context.Update(menuItem);
            return Save();
        }
    }
}
