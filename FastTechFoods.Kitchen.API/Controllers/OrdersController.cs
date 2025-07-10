using FastTechFoods.Kitchen.Application.ViewModel.MenuItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastTechFoods.Kitchen.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetOrders([FromBody] CreateMenuItemViewModel menuItemViewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // TODO: Todos da cozinha pode acessar.

        //await _menuItemService.CreateMenuItemAsync(menuItemViewModel.ToModel());

        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> PutUpdateOrders([FromBody] UpdateMenuItemViewModel menuItemViewModel)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //if (_contatoService.ObterPorId(contato.Id) is null)
        //    return NotFound("Contato não existe");


        // TODO: Todos da cozinha pode acessar.

        //await _menuItemService.UpdateMenuItemAsync(menuItemViewModel.ToModel());

        return NoContent();

    }
}
