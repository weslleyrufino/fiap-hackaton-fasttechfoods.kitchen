using FastTechFoods.Kitchen.Domain.Entities.Base;

namespace FastTechFoods.Kitchen.Domain.Entities;
public class OrderItem : EntityBase
{
    public required Guid MenuItemId { get; set; }

    public required int Quantity { get; set; }

    public required decimal UnitPrice { get; set; }

    public decimal Total => UnitPrice * Quantity;
}

