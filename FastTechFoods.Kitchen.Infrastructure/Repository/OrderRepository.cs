using FastTechFoods.Kitchen.Application.Interfaces.Repository;
using FastTechFoods.Kitchen.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastTechFoods.Kitchen.Infrastructure.Repository;
public class OrderRepository(ApplicationDbContext context) : EFRepository<Order>(context), IOrderRepository
{
    public async Task<Order?> GetOrderByIdAsync(Guid id)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
       

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _dbSet
            .Include(o => o.Items)
            .ToListAsync();
    }

}
