using FastTechFoods.Kitchen.Application.Interfaces.Services;
using FastTechFoods.Kitchen.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace FastTechFoods.Kitchen.Application.Services;
public class MenuItemService : IMenuItemService
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IConfiguration _configuration;

    public MenuItemService(ISendEndpointProvider sendEndpointProvider, IConfiguration config)
    {
        _sendEndpointProvider = sendEndpointProvider;
        _configuration = config;
    }

    public async Task CreateMenuItemAsync(MenuItem menuItem)
    {
        // Inserir na base de dados. 

        // Se gravou com sucesso, colocar a mensagem da fila do rabbitmq.

        var nomeFila = _configuration["MassTransit_CreateItemMenu:NomeFila"];
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
        await endpoint.Send(menuItem);
    }

    public async Task UpdateMenuItemAsync(MenuItem menuItem)
    {
        // Update na base de dados. 

        // Se gravou com sucesso, colocar a mensagem da fila do rabbitmq.

        var nomeFila = _configuration["MassTransit_UpdateItemMenu:NomeFila"];
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
        await endpoint.Send(menuItem);
    }
}
