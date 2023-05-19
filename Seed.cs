using FoodOrderingApi.Data;
using FoodOrderingApi.Model;
using Microsoft.AspNetCore.Identity;

namespace FoodOrderingApi
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category
                    {
                        Id = 1,
                        Name = "Appetizers",
                        Description = "Start your meal off right with one of our delicious appetizers!",
                    }
                };
                dataContext.Categories.AddRange(categories);
                dataContext.SaveChanges();
            }

            if (!dataContext.MenuItems.Any())
            {
                var items = new List<MenuItem>()
                {
                    new MenuItem
                    {
                        Name = "Cheeseburger",
                        Description = "Our classic cheeseburger with your choice of toppings!",
                        Price = 9.99M,
                        ImageUrl = "https://via.placeholder.com/150",
                        IsAvailable = true,
                        CategoryId = 1,
                    }
                };
                dataContext.MenuItems.AddRange(items);
                dataContext.SaveChanges();

            }

        }

    }
}
