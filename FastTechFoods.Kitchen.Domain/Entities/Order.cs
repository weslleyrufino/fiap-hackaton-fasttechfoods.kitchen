using FastTechFoods.Kitchen.Domain.Entities.Base;
using FastTechFoods.Kitchen.Domain.Entities.Enum;

namespace FastTechFoods.Kitchen.Domain.Entities;
public class Order : EntityBase
{
    public required Guid CustomerId { get; set; } // Chave estrangeira

    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public required EnumStatus Status { get; set; }

    public required EnumDeliveryMethod DeliveryMethod { get; set; }

    public string? CancellationReason { get; set; }

    public required List<OrderItem> Items { get; set; } = new();
}
