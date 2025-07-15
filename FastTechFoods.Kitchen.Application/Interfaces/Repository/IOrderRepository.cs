using FastTechFoods.Kitchen.Domain.Entities;

namespace FastTechFoods.Kitchen.Application.Interfaces.Repository;
public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(Guid id);
}
