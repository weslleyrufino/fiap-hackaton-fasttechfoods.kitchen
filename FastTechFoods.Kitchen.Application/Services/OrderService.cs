using FastTechFoods.Kitchen.Application.Interfaces.Services;
using FastTechFoods.Kitchen.Application.ViewModel.Order;
using FastTechFoods.Kitchen.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace FastTechFoods.Kitchen.Application.Services;
public class OrderService : IOrderService
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IConfiguration _configuration;

    public OrderService(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
    {
        _sendEndpointProvider = sendEndpointProvider;
        _configuration = configuration;
    }

    public async Task<Order> GetOrdersAsync()
    {
        throw new NotImplementedException();
    }
    public Task<bool> ExistsAsync(Guid id)
    {
        // Chama a respository pra sabe se existe ou não.
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateOrderAsync(UpdateStatusOrderViewModel orderViewModel)
    {
        // Faz get de Order.

        // Update order recebido com base no orderViewModel.

        // Update na base de dados. 


        // Se gravou com sucesso, colocar a mensagem da fila do rabbitmq.z
        await SendMessageToRabbity(orderViewModel, "MassTransit_UpdateStatusOrder:NomeFila");
    }

    public async Task SendMessageToRabbity(UpdateStatusOrderViewModel orderViewModel, string configuration)// TODO: Deixar essa classe em um lugar centralizado para ser reutilizada por outras classes
    {
        var nomeFila = _configuration[configuration];
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
        await endpoint.Send(orderViewModel);
    }
}
