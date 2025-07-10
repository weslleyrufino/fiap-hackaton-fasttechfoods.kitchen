using FastTechFoods.Kitchen.Domain.Entities;

namespace FastTechFoods.Kitchen.Application.Interfaces.Services;
public interface IContatoService
{
    IEnumerable<Contato> GetContatos();
    Contato ObterPorId(int id);
    IEnumerable<Contato> GetContatosPorDDD(int ddd);
    void PostInserirContato(Contato contato);
    void PutAlterarContato(Contato contato);
    void DeleteContato(int id);
    
}
