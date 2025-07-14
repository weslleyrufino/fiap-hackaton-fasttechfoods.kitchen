using FastTechFoods.Kitchen.Domain.Entities;

namespace FastTechFoods.Kitchen.Application.Interfaces.Repository;
public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<Order?> GetOrderByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task UpdateOrderAsync(Order order);
}
