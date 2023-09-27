namespace FoodOrderingApi.Interfaces
{
    public interface IAdminRepository
    {
        /*
         NAME

            TotalSalesByCategory - Calculate the total sales amount for a specific category.

         SYNOPSIS

            double TotalSalesByCategory(int CategoryId)

         DESCRIPTION

            This function calculates the total sales amount for a specific category based on its CategoryId.

         PARAMETERS

            CategoryId - The unique identifier of the category.

         RETURNS

            Returns the total sales amount for the specified category.
        */
        double TotalSalesByCategory(int CategoryId);

        /*
         NAME

            TotalUser - Get the total number of users.

         SYNOPSIS

            int TotalUser()

         DESCRIPTION

            This function retrieves the total number of users from the database.

         RETURNS

            Returns the total number of users.
        */
        int TotalUser();

        /*
         NAME

            TotalProducts - Get the total number of products.

         SYNOPSIS

            int TotalProducts()

         DESCRIPTION

            This function retrieves the total number of products from the database.

         RETURNS

            Returns the total number of products.
        */
        int TotalProducts();

        /*
         NAME

            TotalCategory - Get the total number of categories.

         SYNOPSIS

            int TotalCategory()

         DESCRIPTION

            This function retrieves the total number of categories from the database.

         RETURNS

            Returns the total number of categories.
        */
        int TotalCategory();

        /*
         NAME

            TotalSalesByMonth - Calculate the total sales amount for a specific month.

         SYNOPSIS

            double TotalSalesByMonth(DateOnly date)

         DESCRIPTION

            This function calculates the total sales amount for a specific month based on the provided date.

         PARAMETERS

            date - The date representing the month for which to calculate sales.

         RETURNS

            Returns the total sales amount for the specified month.
        */
        double TotalSalesByMonth(DateOnly date);

        /*
         NAME

            CategoriesNames - Get a list of category names.

         SYNOPSIS

            List<string> CategoriesNames()

         DESCRIPTION

            This function retrieves a list of category names from the database.

         RETURNS

            Returns a list of category names.
        */
        List<string> CategoriesNames();
    }
}
