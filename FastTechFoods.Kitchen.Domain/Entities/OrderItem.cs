using FastTechFoods.Kitchen.Domain.Entities.Base;

namespace FastTechFoods.Kitchen.Domain.Entities;
public class OrderItem : EntityBase
{
    public required Guid MenuItemId { get; set; }// Menu ForeingKey

    public required int Quantity { get; set; }

    public required decimal UnitPrice { get; set; }

    // Chave estrangeira: cada item pertence a uma ordem
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public decimal Total => UnitPrice * Quantity;

}

