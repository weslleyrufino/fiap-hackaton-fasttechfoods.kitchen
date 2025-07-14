using FastTechFoods.Kitchen.Domain.Entities.Base;

namespace FastTechFoods.Kitchen.Application.Interfaces.Repository;

/// <summary>
/// Interface genérica para CRUD.
/// Quando a minha IRepository for herdada, eu preciso instanciar uma entidade. Como é genérico, defino como "T" que é uma conversão do .NET.
/// Para que mantenha organização do código, forço que essa entidade herde de EntityBase da seguinte forma: where T : EntityBase 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : EntityBase 
{
    T GetById(Guid id);
  

    Task<bool> ExistsAsync(Guid id);
    Task<IList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task InsertAsync(T entidade);
    Task UpdateAsync(T entidade);
    Task RemoveAsync(Guid id);
}
