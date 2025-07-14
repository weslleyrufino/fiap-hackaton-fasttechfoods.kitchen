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

    public async Task<IEnumerable<OrderViewModel>> GetOrdersAsync()
    {
        return (await _orderRepository.GetOrdersAsync()).ToViewModel();
    }


    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _orderRepository.ExistsAsync(id);
    }


    public async Task UpdateOrderAsync(UpdateStatusOrderViewModel orderViewModel)
    {
        // Faz get de Order.
        var order = await GetOrderByIdAsync(orderViewModel.Id);

        // Update order recebido com base no orderViewModel.
        order.Status = orderViewModel.Status.ToDomainStatus();
        order.CancellationReason = orderViewModel.CancellationReason;

        // Update na base de dados. 
        await _orderRepository.UpdateOrderAsync(order.ToModel());

        // TODO: Validar se gravou com sucesso!!

        // Se gravou com sucesso, colocar a mensagem da fila do rabbitmq
        await SendMessageToRabbity(orderViewModel, "MassTransit_UpdateStatusOrder:NomeFila");
    }

    public async Task SendMessageToRabbity(UpdateStatusOrderViewModel orderViewModel, string configuration)// TODO: Deixar essa classe em um lugar centralizado para ser reutilizada por outras classes
    {
        var nomeFila = _configuration[configuration];
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
        await endpoint.Send(orderViewModel);
    }

    public async Task<OrderViewModel?> GetOrderByIdAsync(Guid id)
        => (await _orderRepository.GetOrderByIdAsync(id)).ToViewModel();
}
