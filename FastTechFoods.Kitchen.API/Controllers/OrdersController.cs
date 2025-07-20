using FastTechFoods.Kitchen.Application.Interfaces.Services;
using FastTechFoods.Kitchen.Application.ViewModel.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastTechFoods.Kitchen.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService, ILogger<OrdersController> logger) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;
    private readonly ILogger<OrdersController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // TODO: Permissão. Todos da cozinha podem acessar.

        var result = await _orderService.GetAllOrdersAsync();

        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchUpdateStatusOrders([FromBody] UpdateStatusOrderViewModel orderViewModel)
    {

        // TODO: Permissão. Todos da cozinha podem acessar.

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        bool exists = await _orderService.ExistsAsync(orderViewModel.Id);

        if (!exists)
            return NotFound("Order does not exist.");

        await _orderService.UpdateOrderAsync(orderViewModel);

        return NoContent();

    }
}
