using FastTechFoods.Kitchen.Infrastructure.Repository;
using FastTechFoods.Kitchen.Worker.Messages;
using MassTransit;

namespace FastTechFoods.Kitchen.Worker;
public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly ApplicationDbContext _dbContext;

    public OrderCreatedConsumer(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var msg = context.Message;

        // Mapear e persistir no banco 
        var order = new Domain.Entities.Order
        {
            Id = msg.Id,
            CustomerId = msg.CustomerId,
            CreatedAt = msg.CreatedAt,
            Status = Enum.Parse<Domain.Entities.Enum.EnumStatus>(msg.Status),
            DeliveryMethod = Enum.Parse<Domain.Entities.Enum.EnumDeliveryMethod>(msg.DeliveryMethod),
            CancellationReason = msg.CancellationReason,
            Items = msg.Items.Select(i => new Domain.Entities.OrderItem
            {
                Id = i.Id,
                MenuItemId = i.MenuItemId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                OrderId = msg.Id
            }).ToList()
        };

        _dbContext.Order.Add(order);
        await _dbContext.SaveChangesAsync();

        Console.WriteLine($"[Worker] Order criado: {order.Id}");
    }
}

