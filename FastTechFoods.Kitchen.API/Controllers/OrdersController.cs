using FastTechFoods.Kitchen.Application.Interfaces.Services;
using FastTechFoods.Kitchen.Application.ViewModel.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastTechFoods.Kitchen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService, ILogger<OrdersController> logger) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;
    private readonly ILogger<OrdersController> _logger = logger;

    [HttpGet("GetAllOrders"), Authorize(Roles = "Attendant")]
    public async Task<IActionResult> GetAllOrdersAsync()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _orderService.GetAllOrdersAsync();

        return Ok(result);
    }

    // "O sistema deve permitir que a equipe da cozinha visualize os pedidos recebidos e aceite-os ou recuse-os."
    // Deu a entender que só os atendentes da cozinha que podem acessar essa parte aqui. Pra mim não faz muito sentido o gerente não poder, mas estou deixando assim com base no que entendi do documento.
    [HttpPatch("AcceptOrRejectOrder"), Authorize(Roles = "Attendant")]
    public async Task<IActionResult> AcceptOrRejectOrderAsync([FromBody] UpdateStatusOrderViewModel orderViewModel)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        bool exists = await _orderService.ExistsAsync(orderViewModel.Id);

        if (!exists)
            return NotFound("Order does not exist.");

        await _orderService.UpdateOrderAsync(orderViewModel);

        return NoContent();

    }
}
