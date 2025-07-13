using FastTechFoods.Kitchen.Application.ViewModel.Order;
using FastTechFoods.Kitchen.Domain.Entities;

namespace FastTechFoods.Kitchen.Application.Interfaces.Services;
public interface IOrderService
{
    Task<Order> GetOrdersAsync();
    Task<bool> ExistsAsync(Guid id);
    Task<Order> GetOrderByIdAsync(Guid Id);
    Task UpdateOrderAsync(UpdateStatusOrderViewModel menuItem);
    Task SendMessageToRabbity(UpdateStatusOrderViewModel orderViewModel, string configuration);
}
