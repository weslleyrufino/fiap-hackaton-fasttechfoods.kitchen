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
    IList<T> ObterTodos();
    T ObterPorId(Guid id);
    void Cadastrar(T entidade);
    void Alterar(T entidade);
    void Deletar(Guid id);
}
