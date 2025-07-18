namespace FastTechFoods.Kitchen.Worker.Messages;
public record OrderItemDto(Guid Id, Guid MenuItemId, int Quantity, decimal UnitPrice);
public record OrderCreatedEvent(
    Guid Id,
    string CustomerId,
    DateTime CreatedAt,
    string Status,
    string DeliveryMethod,
    string? CancellationReason,
    List<OrderItemDto> Items
);
