using FastTechFoods.Kitchen.Application.ExtensionMethods;
using FastTechFoods.Kitchen.Application.Interfaces.Repository;
using FastTechFoods.Kitchen.Application.Interfaces.Services;
using FastTechFoods.Kitchen.Application.ViewModel.MenuItem;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace FastTechFoods.Kitchen.Application.Services;
public class MenuItemService(ISendEndpointProvider sendEndpointProvider, IConfiguration config, IMenuItemRepository menuItemRepository) : IMenuItemService
{
    private readonly ISendEndpointProvider _sendEndpointProvider = sendEndpointProvider;
    private readonly IConfiguration _configuration = config;
    private readonly IMenuItemRepository _menuItemRepository = menuItemRepository;

    public async Task CreateMenuItemAsync(CreateMenuItemViewModel menuItem)
    {
        // Inserir na base de dados. 
        await _menuItemRepository.InsertAsync(menuItem.ToModel());

        // Se gravou com sucesso, colocar a mensagem da fila do rabbitmq.

        var nomeFila = _configuration["MassTransit_CreateItemMenu:NomeFila"];
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
        await endpoint.Send(menuItem);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _menuItemRepository.ExistsAsync(id);
    }

    public async Task UpdateMenuItemAsync(UpdateMenuItemViewModel menuItem)
    {
        var existingMenuItem = await _menuItemRepository.GetByIdAsync(menuItem.Id);

        if (existingMenuItem is null)
            throw new Exception("Menu item not found.");

        existingMenuItem.UpdateFrom(menuItem);


        // Update na base de dados. 
        await _menuItemRepository.UpdateAsync(existingMenuItem);

        // Se gravou com sucesso, colocar a mensagem da fila do rabbitmq.

        var nomeFila = _configuration["MassTransit_UpdateItemMenu:NomeFila"];
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
        await endpoint.Send(menuItem);
    }
}
