using FastTechFoods.Kitchen.Application.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using FastTechFoods.Kitchen.Domain.Entities.Base;

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

    public void Alterar(T entidade)
    {
        _dbSet.Update(entidade);
        _context.SaveChanges();
    }

    public void Cadastrar(T entidade)
    {
        _dbSet.Add(entidade);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        _dbSet.Remove(ObterPorId(id));
        _context.SaveChanges();
    }

    public T ObterPorId(Guid id) 
        => _dbSet.FirstOrDefault(entity => entity.Id == id);

    public IList<T> ObterTodos()
        => _dbSet.ToList();
}
