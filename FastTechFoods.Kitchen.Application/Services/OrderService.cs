using FastTechFoods.Kitchen.Application.Common;
using FastTechFoods.Kitchen.Application.ExtensionMethods;
using FastTechFoods.Kitchen.Application.Interfaces.Repository;
using FastTechFoods.Kitchen.Application.Interfaces.Services;
using FastTechFoods.Kitchen.Application.ViewModel.Order;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace FastTechFoods.Kitchen.Application.Services;
public class OrderService(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration, IOrderRepository orderRepository) : IOrderService
{
    private readonly ISendEndpointProvider _sendEndpointProvider = sendEndpointProvider;
    private readonly IConfiguration _configuration = configuration;
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync()
        => (await _orderRepository.GetAllOrdersAsync()).ToViewModel();


    public async Task<bool> ExistsAsync(Guid id)
        => await _orderRepository.ExistsAsync(id);


    public async Task UpdateOrderAsync(UpdateStatusOrderViewModel orderViewModel)
    {
        var order = await SetData(orderViewModel);

        // Update na base de dados. 
        await _orderRepository.UpdateAsync(order.ToModel());

        // Se gravou com sucesso, colocar a mensagem da fila do rabbitmq
        await RabbitMqHelper.SendMessageAsync(_sendEndpointProvider, _configuration, orderViewModel.ToModelEvent(), "MassTransit_AcceptedOrRejectedOrderByKitchen:NomeFila");
    }

    private async Task<OrderViewModel> SetData(UpdateStatusOrderViewModel orderViewModel)
    {
        // Faz get de Order.
        var order = await GetOrderByIdAsync(orderViewModel.Id);

        if (order is null)
            throw new Exception("Order not found.");

        // Update order recebido com base no orderViewModel.
        order.Status = orderViewModel.Status;
        order.CancellationReason = orderViewModel.CancellationReason;

        return order;
    }

    public async Task<OrderViewModel?> GetOrderByIdAsync(Guid id)
        => (await _orderRepository.GetOrderByIdAsync(id))?.ToViewModel();
}
