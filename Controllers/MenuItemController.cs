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
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MenuItem>))]
        public IActionResult GetMenuItems()
        {
            var menuItems = _mapper.Map<List<MenuItemDto>>(_menuItemRepository.GetMenuItems());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(menuItems);
        }
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
        [HttpGet("{menuItemId}/category")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int menuItemId)
        {
            if (!_menuItemRepository.MenuItemExists(menuItemId))
                return NotFound();

            var category = _mapper.Map<Category>(
                _menuItemRepository. GetCategory( menuItemId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }
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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMenuItem([FromQuery] int caretogoryId, [FromBody] MenuItemDto menuItemCreate)
        {
            if (menuItemCreate == null)
                return BadRequest(ModelState);

            var owners = _menuItemRepository.GetMenuItems()
                .Where(c => c.Name.Trim().ToUpper() == menuItemCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (owners != null)
            {
                ModelState.AddModelError("", "MenuItem already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var menuItemMap = _mapper.Map<MenuItem>(menuItemCreate);

            menuItemMap.CategoryId = caretogoryId;

            if (!_menuItemRepository.CreateMenuItem(menuItemMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

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

            var ownerMap = _mapper.Map<MenuItem>(updatedMenuItem);

            if (!_menuItemRepository.UpdateMenuItem(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

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

            var ownerToDelete = _menuItemRepository.GetMenuItem(menuItemId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_menuItemRepository.DeleteMenuItem(ownerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting MenuItem");
            }

            return NoContent();
        }

    }
}
