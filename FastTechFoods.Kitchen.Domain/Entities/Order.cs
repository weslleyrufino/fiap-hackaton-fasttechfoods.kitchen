using FastTechFoods.Kitchen.Domain.Entities.Base;

namespace FastTechFoods.Kitchen.Domain.Entities;
public class Order : EntityBase
{
    public required string CustomerId { get; set; }

    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public required string Status { get; set; }

    public required string DeliveryMethod { get; set; }

    public string? CancellationReason { get; set; }

    public required List<OrderItem> Items { get; set; } = new();
}
