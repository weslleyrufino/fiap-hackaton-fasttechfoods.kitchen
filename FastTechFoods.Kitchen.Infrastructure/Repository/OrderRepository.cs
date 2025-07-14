using FastTechFoods.Kitchen.Application.Interfaces.Repository;
using FastTechFoods.Kitchen.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastTechFoods.Kitchen.Infrastructure.Repository;
public class OrderRepository(ApplicationDbContext context) : EFRepository<Order>(context), IOrderRepository
{
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbSet
            .AsNoTracking()
            .AnyAsync(o => o.Id == id);
    }
        

    public async Task<Order?> GetOrderByIdAsync(Guid id)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
       

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return await _dbSet
            .Include(o => o.Items)
            .ToListAsync();
    }

    public async Task UpdateOrderAsync(Order order)
    {
        _dbSet.Update(order);
        await _context.SaveChangesAsync();
    }
}
