using FastTechFoods.Kitchen.Application.Interfaces.Services;
using FastTechFoods.Kitchen.Application.ViewModel.MenuItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastTechFoods.Kitchen.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MenuItemController(IMenuItemService menuItemService, ILogger<MenuItemController> logger) : ControllerBase
{
    private readonly IMenuItemService _menuItemService = menuItemService;
    private readonly ILogger<MenuItemController> _logger = logger;


    [HttpPost, Authorize(Roles = "Manager")]
    public async Task<IActionResult> PostCreateMenuItem([FromBody] CreateMenuItemViewModel menuItemViewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _menuItemService.CreateMenuItemAsync(menuItemViewModel);

        return Created();
    }

    [HttpPut, Authorize(Roles = "Manager")]
    public async Task<IActionResult> PutUpdateMenuItem([FromBody] UpdateMenuItemViewModel menuItemViewModel)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var exists = await _menuItemService.ExistsAsync(menuItemViewModel.Id);

        if (!exists)
            return NotFound("Menu Item not found.");


        await _menuItemService.UpdateMenuItemAsync(menuItemViewModel);

        return NoContent();

    }
}
