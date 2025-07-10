using FastTechFoods.Kitchen.Domain.Entities;
using FastTechFoods.Kitchen.Domain.Entities;

namespace FastTechFoods.Kitchen.Application.Interfaces.Repository;
public interface IContatoRepository : IRepository<Contato>
{
    IEnumerable<Contato> GetContatosPorDDD(int ddd);

    IEnumerable<Contato> GetTodosContatosMesclandoComDDD();

}
