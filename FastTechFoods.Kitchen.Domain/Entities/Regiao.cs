using FastTechFoods.Kitchen.Domain.Entities.Base;

namespace FastTechFoods.Kitchen.Domain.Entities;
public class Regiao : EntityBase
{
    public string Name { get; set; }
    public required int DDD { get; set; }
}
