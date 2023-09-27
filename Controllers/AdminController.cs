using System;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using FoodOrderingApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private IUserRepository _userRepository;
        private ICategoryRepository _categoryRepository;
        private IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _adminRepository = adminRepository;
        }

        /*
            NAME

            IActionResult::GetTotalCategory - Get the total number of categories.

            SYNOPSIS

            [HttpGet("totalCategory")]
            public IActionResult GetTotalCategory()

            DESCRIPTION

            This function retrieves the total number of categories and returns it as an integer.
            It returns a 200 OK response with the total number of categories if successful.

            RETURNS

            Returns a 200 OK response with the total number of categories (integer) if successful.
            Returns a 400 Bad Request response in case of an exception.
        */
        [HttpGet("totalCategory")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalCategory()
        {
            try
            {
                int totalCategory = _adminRepository.TotalCategory();
                return Ok(totalCategory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        /*
            NAME

            IActionResult::GetTotalSalesByMonth - Get the total sales amount for a specific month.

            SYNOPSIS

            [HttpGet("totalSalesByMonth")]
            public IActionResult GetTotalSalesByMonth(DateOnly month)

            DESCRIPTION

            This function calculates the total sales amount for a specific month and returns it as a double.
            It validates the month and returns a 200 OK response with the total sales amount if successful.

            PARAMETERS

            DateOnly month - The month for which to calculate total sales.

            RETURNS

            Returns a 200 OK response with the total sales amount (double) if successful.
            Returns a 400 Bad Request response in case of an exception.
        */
        [HttpGet("totalSalesByMonth")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalSalesByMonth(DateOnly month)
        {
            try
            {
                // Validate the month
                double amount = _adminRepository.TotalSalesByMonth(month);
                return Ok(amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        /*
            NAME

            IActionResult::GetTotalUser - Get the total number of users.

            SYNOPSIS

            [HttpGet("totalUser")]
            public IActionResult GetTotalUser()

            DESCRIPTION

            This function retrieves the total number of users and returns it as an integer.
            It returns a 200 OK response with the total number of users if successful.

            RETURNS

            Returns a 200 OK response with the total number of users (integer) if successful.
            Returns a 400 Bad Request response in case of an exception.
        */
        [HttpGet("totalUser")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalUser()
        {
            try
            {
                int totalUser = _adminRepository.TotalUser();
                return Ok(totalUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        /*
            NAME

            IActionResult::GetTotalSalesByCategory - Get the total sales amount for a specific category.

            SYNOPSIS

            [HttpGet("totalSalesByCategory")]
            public IActionResult GetTotalSalesByCategory(int categoryId)

            DESCRIPTION

            This function calculates the total sales amount for a specific category and returns it as a double.
            It validates the category's existence and returns a 200 OK response with the total sales amount if successful.

            PARAMETERS

            int categoryId - The ID of the category for which to calculate total sales.

            RETURNS

            Returns a 200 OK response with the total sales amount (double) if successful.
            Returns a 400 Bad Request response if the category does not exist or in case of an exception.
        */
        [HttpGet("totalSalesByCategory")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalSalesByCategory(int categoryId)
        {
            try
            {
                if (!_categoryRepository.CategoryExists(categoryId))
                {
                    return BadRequest("Category does not exist");
                }
                double total = _adminRepository.TotalSalesByCategory(categoryId);
                return Ok(total);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        /*
            NAME

            IActionResult::GetTotalSalesByCategory - Get the total sales amount for a specific category by name.

            SYNOPSIS

            [HttpPost("totalSalesByCategoryName")]
            public IActionResult GetTotalSalesByCategory(string categoryName)

            DESCRIPTION

            This function calculates the total sales amount for a specific category by name and returns it as a double.
            It validates the category name, retrieves its ID, and calculates the total sales.
            It returns a 200 OK response with the total sales amount if successful.

            PARAMETERS

            string categoryName - The name of the category for which to calculate total sales.

            RETURNS

            Returns a 200 OK response with the total sales amount (double) if successful.
            Returns a 400 Bad Request response if the category name is invalid or in case of an exception.
        */
        [HttpPost("totalSalesByCategoryName")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalSalesByCategory(string categoryName)
        {
            try
            {
                int categoryId = _categoryRepository.GetIdByName(categoryName);
                if (categoryId == null)
                    return BadRequest();
                double total = _adminRepository.TotalSalesByCategory(categoryId);
                return Ok(total);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        /*
            NAME

            IActionResult::GetTotalProducts - Get the total number of products.

            SYNOPSIS

            [HttpGet("totalProducts")]
            public IActionResult GetTotalProducts()

            DESCRIPTION

            This function retrieves the total number of products and returns it as an integer.
            It returns a 200 OK response with the total number of products if successful.

            RETURNS

            Returns a 200 OK response with the total number of products (integer) if successful.
            Returns a 400 Bad Request response in case of an exception.
        */
        [HttpGet("totalProducts")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalProducts()
        {
            try
            {
                int totalProduct = _adminRepository.TotalProducts();
                return Ok(totalProduct);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        /*
            NAME

            IActionResult::GetCategoriesNames - Get the names of all categories.

            SYNOPSIS

            [HttpGet("categoryName")]
            public IActionResult GetCategoriesNames()

            DESCRIPTION

            This function retrieves the names of all categories and returns them as a list of strings.
            It returns a 200 OK response with the category names if successful.

            RETURNS

            Returns a 200 OK response with a list of category names (List<string>) if successful.
            Returns a 400 Bad Request response in case of an exception.
        */
        [HttpGet("categoryName")]
        [ProducesResponseType(200, Type = typeof(List<string>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoriesNames()
        {
            try
            {
                List<string> totalCategoryNames = _adminRepository.CategoriesNames();
                return Ok(totalCategoryNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}