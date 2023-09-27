using AutoMapper;
using FoodOrderingApi.Dto;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using FoodOrderingApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /*
        NAME

        GetCategories - Retrieves all categories.

        SYNOPSIS

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategories()

        DESCRIPTION

        This method retrieves all categories and returns them as a list of CategoryDto objects.
        It returns a 200 OK response if successful.

        RETURNS

        Returns a 200 OK response with a list of CategoryDto objects if successful.
        Returns a 400 Bad Request response if the request is invalid.
        */
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        /*
        NAME

        GetCategoryById - Retrieves a category by ID.

        SYNOPSIS

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoryById(int id)

        DESCRIPTION

        This method retrieves a category by its ID and returns it as a CategoryDto object.
        It returns a 200 OK response if successful.

        PARAMETERS

        id - An integer representing the ID of the category to retrieve.

        RETURNS

        Returns a 200 OK response with a CategoryDto object if successful.
        Returns a 400 Bad Request response if the request is invalid.
        Returns a 404 Not Found response if the category is not found.
        */
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoryById(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
                return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategoryById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        /*
        NAME

        GetMenuItemsByCategory - Retrieves menu items by category.

        SYNOPSIS

        [HttpGet("{categoryId}/MenuItem")]
        [ProducesResponseType(200, Type = typeof(List<MenuItemDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetMenuItemsByCategory(int categoryId)

        DESCRIPTION

        This method retrieves menu items by category and returns them as a list of MenuItemDto objects.
        It returns a 200 OK response if successful.

        PARAMETERS

        categoryId - An integer representing the ID of the category.

        RETURNS

        Returns a 200 OK response with a list of MenuItemDto objects if successful.
        Returns a 400 Bad Request response if the request is invalid.
        Returns a 404 Not Found response if the category is not found.
        */
        [HttpGet("{categoryId}/MenuItem")]
        [ProducesResponseType(200, Type = typeof(List<MenuItemDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetMenuItemsByCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var menuItems = _mapper.Map<List<MenuItemDto>>(_categoryRepository.GetMenuItemsByCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(menuItems);
        }

        /*
        NAME

        CreateCategory - Creates a new category.

        SYNOPSIS

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)

        DESCRIPTION

        This method creates a new category and returns a 204 No Content response if successful.
        It returns a 400 Bad Request response if the request is invalid.
        It returns a 422 Unprocessable Entity response if the category already exists.

        PARAMETERS

        categoryCreate - A CategoryDto object representing the category to create.

        RETURNS

        Returns a 204 No Content response if successful.
        Returns a 400 Bad Request response if the request is invalid.
        Returns a 422 Unprocessable Entity response if the category already exists.
        */
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var owners = _categoryRepository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (owners != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        /*
        NAME

        UpdateCategory - Updates an existing category.

        SYNOPSIS

        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)

        DESCRIPTION

        This method updates an existing category and returns a 204 No Content response if successful.
        It returns a 400 Bad Request response if the request is invalid.
        It returns a 404 Not Found response if the category is not found.

        PARAMETERS

        categoryId - An integer representing the ID of the category to update.
        updatedCategory - A CategoryDto object representing the updated category data.

        RETURNS

        Returns a 204 No Content response if successful.
        Returns a 400 Bad Request response if the request is invalid.
        Returns a 404 Not Found response if the category is not found.
        */
        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null)
                return BadRequest(ModelState);

            if (categoryId != updatedCategory.Id)
                return BadRequest(ModelState);

            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = _mapper.Map<Category>(updatedCategory);

            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /*
        NAME

        DeleteCategory - Deletes a category.

        SYNOPSIS

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)

        DESCRIPTION

        This method deletes a category and returns a 204 No Content response if successful.
        It returns a 400 Bad Request response if the request is invalid.
        It returns a 404 Not Found response if the category is not found.

        PARAMETERS

        categoryId - An integer representing the ID of the category to delete.

        RETURNS

        Returns a 204 No Content response if successful.
        Returns a 400 Bad Request response if the request is invalid.
        Returns a 404 Not Found response if the category is not found.
        */
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            Category categoryToDelete = _categoryRepository.GetCategoryById(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}
