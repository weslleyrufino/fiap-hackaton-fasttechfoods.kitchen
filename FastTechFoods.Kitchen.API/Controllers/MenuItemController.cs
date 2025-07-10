using FastTechFoods.Kitchen.Application.ExtensionMethods;
using FastTechFoods.Kitchen.Application.Interfaces.Services;
using FastTechFoods.Kitchen.Application.ViewModel.MenuItem;
using Microsoft.AspNetCore.Mvc;

namespace FastTechFoods.Kitchen.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MenuItemController(IMenuItemService menuItemService, ILogger<MenuItemController> logger) : ControllerBase
{
    private readonly IMenuItemService _menuItemService = menuItemService;
    private readonly ILogger<MenuItemController> _logger = logger;


    [HttpPost]
    public async Task<IActionResult> PostCreateMenuItem([FromBody] CreateMenuItemViewModel menuItemViewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // TODO: SOMENTE GERENTE PODE ACESSAR.

        await _menuItemService.CreateMenuItemAsync(menuItemViewModel.ToModel());

        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> PutUpdateMenuItem([FromBody] UpdateMenuItemViewModel menuItemViewModel)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //if (_contatoService.ObterPorId(contato.Id) is null)
        //    return NotFound("Contato não existe");


        // TODO: SOMENTE GERENTE PODE ACESSAR.

        await _menuItemService.UpdateMenuItemAsync(menuItemViewModel.ToModel());

        return NoContent();

    }
}
