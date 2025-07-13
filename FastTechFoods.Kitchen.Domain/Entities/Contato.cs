using FastTechFoods.Kitchen.Domain.Entities.Base;

namespace FastTechFoods.Kitchen.Domain.Entities;
public class Contato : EntityBase
{
    public string Name { get; set; }
    public required string Telefone { get; set; }
    public required string Email { get; set; }
    public required int RegiaoId { get; set; }
    public Regiao Regiao { get; set; }
}
