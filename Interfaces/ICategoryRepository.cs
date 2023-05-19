using FoodOrderingApi.Model;

namespace FoodOrderingApi.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategoryById(int id);
        ICollection<MenuItem> GetMenuItemsByCategory(int  categoryId);
        bool CategoryExists(int categoryId);
        bool CreateCategory(Category category);
        bool DeleteCategory(Category category);
        bool UpdateCategory(Category category);

        bool Save();


    }
}
