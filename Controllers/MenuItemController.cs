using AutoMapper;
using FoodOrderingApi.Dto;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using FoodOrderingApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IMapper _mapper;
        private ICategoryRepository _categoryRepository;
        private IMenuItemRepository _menuItemRepository;

        public MenuItemController(IMenuItemRepository menuItemRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }
        /*
            NAME

            IActionResult::GetMenuItems - Get a list of MenuItems from the database.

            SYNOPSIS

            IActionResult GetMenuItems()

            DESCRIPTION

            This function retrieves a list of MenuItems from the database by calling the repository. 
            It then maps these MenuItems to DTOs and returns them as a JSON response.

            RETURNS

            Returns a JSON list of MenuItems if the database call was successful.
            Returns BadRequest if there was an issue with the request or data.
        */
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MenuItem>))]
        public IActionResult GetMenuItems()
        {
            var menuItems = _mapper.Map<List<MenuItemDto>>(_menuItemRepository.GetMenuItems());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(menuItems);
        }

        /*
            NAME

            IActionResult::GetMenuItem - Get a specific MenuItem by ID from the database.

            SYNOPSIS

            IActionResult GetMenuItem(int menuItemId)

            DESCRIPTION

            This function retrieves a specific MenuItem from the database based on the provided menuItemId.
            It checks if the MenuItem exists and then maps it to a DTO before returning it as a JSON response.

            RETURNS

            Returns a JSON representation of the requested MenuItem if it exists.
            Returns NotFound if the MenuItem with the provided ID does not exist.
            Returns BadRequest if there was an issue with the request or data.
        */
        [HttpGet("{menuItemId}")]
        [ProducesResponseType(200, Type = typeof(MenuItem))]
        [ProducesResponseType(400)]
        public IActionResult GetMenuItem(int menuItemId)
        {
            if (!_menuItemRepository.MenuItemExists(menuItemId))
                return NotFound();

            var menuItem = _mapper.Map<MenuItemDto>(_menuItemRepository.GetMenuItem(menuItemId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(menuItem);
        }

        /*
            NAME

            IActionResult::GetCategory - Get the category associated with a specific MenuItem.

            SYNOPSIS

            IActionResult GetCategory(int menuItemId)

            DESCRIPTION

            This function retrieves the category associated with a specific MenuItem in the database,
            based on the provided menuItemId. It checks if the MenuItem exists, gets its associated category,
            and maps it before returning it as a JSON response.

            RETURNS

            Returns a JSON representation of the associated Category if both MenuItem and Category exist.
            Returns NotFound if the MenuItem with the provided ID does not exist.
            Returns BadRequest if there was an issue with the request or data.
        */
        [HttpGet("{menuItemId}/category")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int menuItemId)
        {
            if (!_menuItemRepository.MenuItemExists(menuItemId))
                return NotFound();

            var category = _mapper.Map<Category>(_menuItemRepository.GetCategory(menuItemId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        /*
            NAME

            IActionResult::GetMenuItemsByCategory - Get a list of MenuItems by Category from the database.

            SYNOPSIS

            IActionResult GetMenuItemsByCategory(int categoryId)

            DESCRIPTION

            This function retrieves a list of MenuItems from the database that belong to a specific Category,
            based on the provided categoryId. It checks if the Category exists, gets the associated MenuItems,
            and maps them to DTOs before returning them as a JSON response.

            RETURNS

            Returns a JSON list of MenuItems if the Category exists and contains MenuItems.
            Returns NotFound if the Category with the provided ID does not exist.
            Returns BadRequest if there was an issue with the request or data.
        */
        [HttpGet("{categoryId}/menuItems-by-category")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetMenuItemsByCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var menuItems = _mapper.Map<List<MenuItemDto>>(_menuItemRepository.GetMenuItemByCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(menuItems);
        }

        /*
            NAME

            IActionResult::CreateMenuItem - Create a new MenuItem and store it in the database.

            SYNOPSIS

            IActionResult CreateMenuItem([FromBody] MenuItemDto menuItemCreate)

            DESCRIPTION

            This function creates a new MenuItem in the database based on the provided MenuItemDto.
            It performs validation checks, including checking for existing duplicate MenuItems.
            If the MenuItem is successfully created, it returns a success message.

            RETURNS

            Returns "Successfully created" if the MenuItem is created successfully.
            Returns BadRequest if there was an issue with the request or data.
            Returns StatusCode 422 if a duplicate MenuItem already exists.
        */
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMenuItem([FromBody] MenuItemDto menuItemCreate)
        {
            if (menuItemCreate == null)
                return BadRequest(ModelState);

            var existingMenuItem = _menuItemRepository.GetMenuItems()
                .Where(c => c.Name.Trim().ToUpper() == menuItemCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (existingMenuItem != null)
            {
                ModelState.AddModelError("", "MenuItem already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var menuItemMap = _mapper.Map<MenuItem>(menuItemCreate);

            if (!_menuItemRepository.CreateMenuItem(menuItemMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        /*
            NAME

            IActionResult::UpdateMenuItem - Update an existing MenuItem in the database.

            SYNOPSIS

            IActionResult UpdateMenuItem(int menuItemId, [FromBody] MenuItemDto updatedMenuItem)

            DESCRIPTION

            This function updates an existing MenuItem in the database based on the provided menuItemId and updatedMenuItem.
            It performs validation checks, including checking if the MenuItem exists.
            If the update is successful, it returns NoContent.

            RETURNS

            Returns NoContent if the MenuItem is successfully updated.
            Returns BadRequest if there was an issue with the request or data.
            Returns NotFound if the MenuItem with the provided ID does not exist.
        */
        [HttpPut("{menuItemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMenuItem(int menuItemId, [FromBody] MenuItemDto updatedMenuItem)
        {
            if (updatedMenuItem == null)
                return BadRequest(ModelState);

            if (menuItemId != updatedMenuItem.Id)
                return BadRequest(ModelState);

            if (!_menuItemRepository.MenuItemExists(menuItemId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var menuItemMap = _mapper.Map<MenuItem>(updatedMenuItem);

            if (!_menuItemRepository.UpdateMenuItem(menuItemMap))
            {
                ModelState.AddModelError("", "Something went wrong updating MenuItem");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /*
            NAME

            IActionResult::DeleteMenuItem - Delete an existing MenuItem from the database.

            SYNOPSIS

            IActionResult DeleteMenuItem(int menuItemId)

            DESCRIPTION

            This function deletes an existing MenuItem from the database based on the provided menuItemId.
            It performs validation checks, including checking if the MenuItem exists.
            If the deletion is successful, it returns NoContent.

            RETURNS

            Returns NoContent if the MenuItem is successfully deleted.
            Returns BadRequest if there was an issue with the request or data.
            Returns NotFound if the MenuItem with the provided ID does not exist.
        */
        [HttpDelete("{menuItemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMenuItem(int menuItemId)
        {
            if (!_menuItemRepository.MenuItemExists(menuItemId))
            {
                return NotFound();
            }

            var itemToDelete = _menuItemRepository.GetMenuItem(menuItemId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_menuItemRepository.DeleteMenuItem(itemToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting MenuItem");
            }

            return NoContent();
        }
    }
}
