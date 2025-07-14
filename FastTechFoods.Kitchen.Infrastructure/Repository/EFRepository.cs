using FastTechFoods.Kitchen.Application.Interfaces.Repository;
using FastTechFoods.Kitchen.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace FastTechFoods.Kitchen.Infrastructure.Repository;
public class EFRepository<T> : IRepository<T> where T : EntityBase
{
    protected ApplicationDbContext _context;
    protected DbSet<T> _dbSet;

    public EFRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public T GetById(Guid id) 
        => _dbSet.FirstOrDefault(entity => entity.Id == id);

    
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbSet
            .AsNoTracking()
            .AnyAsync(o => o.Id == id);
    }

    public async Task<IList<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task InsertAsync(T entidade)
    {
        _dbSet.Add(entidade);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entidade)
    {
        _dbSet.Update(entidade);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        _dbSet.Remove(GetById(id));
        await _context.SaveChangesAsync();
    }
}
