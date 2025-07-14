using FastTechFoods.Kitchen.Application.ViewModel.Order;

namespace FastTechFoods.Kitchen.Application.Interfaces.Services;
public interface IOrderService
{
    Task<IEnumerable<OrderViewModel>> GetOrdersAsync();
    Task<bool> ExistsAsync(Guid id);
    Task<OrderViewModel?> GetOrderByIdAsync(Guid id);
    Task UpdateOrderAsync(UpdateStatusOrderViewModel menuItem);
    Task SendMessageToRabbity(UpdateStatusOrderViewModel orderViewModel, string configuration);
}
