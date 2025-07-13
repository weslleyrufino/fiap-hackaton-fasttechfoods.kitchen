using FastTechFoods.Kitchen.Application.Interfaces.Services;
using FastTechFoods.Kitchen.Application.ViewModel.Order;
using Microsoft.AspNetCore.Mvc;

namespace FastTechFoods.Kitchen.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService, ILogger<MenuItemController> logger) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;
    private readonly ILogger<MenuItemController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetOrders([FromBody] OrderViewlModel orderViewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // TODO: Todos da cozinha pode acessar.

        //await _menuItemService.CreateMenuItemAsync(menuItemViewModel.ToModel());

        return Created();
    }

    [HttpPatch]
    public async Task<IActionResult> PatchUpdateStatusOrders([FromBody] UpdateStatusOrderViewModel orderViewModel)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //bool exists = await _orderService.ExistsAsync(orderViewModel.Id);

        //if (!exists)
        //    return NotFound("Order does not exist");

        await _orderService.UpdateOrderAsync(orderViewModel);

        return NoContent();

    }
}
