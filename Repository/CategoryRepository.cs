using AutoMapper;
using FoodOrderingApi.Data;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;

namespace FoodOrderingApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

         /*
         NAME

            CategoryExists - Checks if a category with the specified ID exists.

         DESCRIPTION

            This method checks if a category with the specified ID exists in the data context.

         PARAMETERS

            categoryId - An integer representing the ID of the category to check.

         RETURNS

            Returns true if a category with the specified ID exists; otherwise, false.
        */
        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.Id == categoryId);
        }

                /*
         NAME

            CreateCategory - Creates a new category.

         DESCRIPTION

            This method adds a new category to the data context.

         PARAMETERS

            category - The Category object to be created.

         RETURNS

            Returns true if the category creation was successful; otherwise, false.
         */
        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

                /*
         NAME

            DeleteCategory - Deletes a category.

         DESCRIPTION

            This method removes a category from the data context.

         PARAMETERS

            category - The Category object to be deleted.

         RETURNS

            Returns true if the category deletion was successful; otherwise, false.
         */
        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

                /*
         NAME

            GetCategories - Retrieves a list of all categories.

         DESCRIPTION

            This method retrieves a list of all categories from the data context.

         RETURNS

            Returns a collection of Category objects representing all categories.
         */
        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

                 /*
         NAME

            GetCategoryById - Retrieves a category by its ID.

         DESCRIPTION

            This method retrieves a category with the specified ID from the data context.

         PARAMETERS

            id - An integer representing the ID of the category to retrieve.

         RETURNS

            Returns a Category object representing the category with the specified ID.
         */
        public Category GetCategoryById(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }




            /*
         NAME

            GetIdByName - Retrieves the ID of a category by its name.

         DESCRIPTION

            This method retrieves the ID of a category with the specified name from the data context.

         PARAMETERS

            name - A string representing the name of the category to retrieve.

         RETURNS

            Returns an integer representing the ID of the category with the specified name.
     */
        public int GetIdByName(string name)
        {
            return (int)_context.Categories.Where(c => c.Name == name).FirstOrDefault().Id;
        }

                /*
         NAME

            GetMenuItemsByCategory - Retrieves menu items in a category.

         DESCRIPTION

            This method retrieves a list of menu items within the specified category from the data context.

         PARAMETERS

            categoryId - An integer representing the ID of the category to retrieve menu items for.

         RETURNS

            Returns a collection of MenuItem objects representing menu items in the specified category.
         */
        public ICollection<MenuItem> GetMenuItemsByCategory(int categoryId)
        {
            return _context.MenuItems.Where(c => c.CategoryId == categoryId).ToList();
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

        UpdateCategory - Updates a category.

     DESCRIPTION

        This method updates a category in the data context.

     PARAMETERS

        category - The Category object to be updated.

     RETURNS

        Returns true if the update operation was successful; otherwise, false.
     */
        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
