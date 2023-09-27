using FoodOrderingApi.Model;

namespace FoodOrderingApi.Interfaces
{
    public interface ICategoryRepository
    {
        /*
         NAME

            GetCategories - Retrieve a collection of all categories.

         SYNOPSIS

            ICollection<Category> GetCategories()

         DESCRIPTION

            This function retrieves a collection of all categories from the database.

         RETURNS

            Returns a collection of Category objects if the database call was successful.
        */
        ICollection<Category> GetCategories();

        /*
         NAME

            GetCategoryById - Retrieve a category by its unique identifier.

         SYNOPSIS

            Category GetCategoryById(int id)

         DESCRIPTION

            This function retrieves a category from the database by its unique identifier.

         PARAMETERS

            id - The unique identifier of the category to retrieve.

         RETURNS

            Returns the Category object with the specified id, or null if not found.
        */
        Category GetCategoryById(int id);

        /*
         NAME

            GetMenuItemsByCategory - Retrieve a collection of menu items belonging to a specific category.

         SYNOPSIS

            ICollection<MenuItem> GetMenuItemsByCategory(int categoryId)

         DESCRIPTION

            This function retrieves a collection of menu items that belong to a specific category.

         PARAMETERS

            categoryId - The unique identifier of the category.

         RETURNS

            Returns a collection of MenuItem objects in the specified category.
        */
        ICollection<MenuItem> GetMenuItemsByCategory(int categoryId);

        /*
         NAME

            CategoryExists - Check if a category with the specified categoryId exists.

         SYNOPSIS

            bool CategoryExists(int categoryId)

         DESCRIPTION

            This function checks if a category with the specified categoryId exists in the database.

         PARAMETERS

            categoryId - The unique identifier of the category to check.

         RETURNS

            Returns true if the category exists, false otherwise.
        */
        bool CategoryExists(int categoryId);

        /*
         NAME

            CreateCategory - Create a new category.

         SYNOPSIS

            bool CreateCategory(Category category)

         DESCRIPTION

            This function creates a new category in the database.

         PARAMETERS

            category - The category to be created.

         RETURNS

            Returns true if the category creation was successful, false otherwise.
        */
        bool CreateCategory(Category category);

        /*
         NAME

            DeleteCategory - Delete an existing category.

         SYNOPSIS

            bool DeleteCategory(Category category)

         DESCRIPTION

            This function deletes an existing category from the database.

         PARAMETERS

            category - The category to be deleted.

         RETURNS

            Returns true if the category deletion was successful, false otherwise.
        */
        bool DeleteCategory(Category category);

        /*
         NAME

            UpdateCategory - Update an existing category.

         SYNOPSIS

            bool UpdateCategory(Category category)

         DESCRIPTION

            This function updates an existing category in the database.

         PARAMETERS

            category - The category with updated information.

         RETURNS

            Returns true if the category update was successful, false otherwise.
        */
        bool UpdateCategory(Category category);

        /*
         NAME

            GetIdByName - Retrieve the unique identifier of a category by its name.

         SYNOPSIS

            int GetIdByName(string name)

         DESCRIPTION

            This function retrieves the unique identifier of a category by its name.

         PARAMETERS

            name - The name of the category.

         RETURNS

            Returns the unique identifier of the category with the specified name.
        */
        int GetIdByName(string name);

        /*
         NAME

            Save - Save changes to the database.

         SYNOPSIS

            bool Save()

         DESCRIPTION

            This function saves any changes made to the database.

         RETURNS

            Returns true if the changes were successfully saved, false otherwise.
        */
        bool Save();
    }
}
