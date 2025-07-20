using FastTechFoods.Contracts;
using FastTechFoods.Kitchen.Domain.Entities;
using FastTechFoods.Kitchen.Domain.Entities.Enum;
using FastTechFoods.Kitchen.Infrastructure.Repository;
using MassTransit;

namespace FastTechFoods.Kitchen.Worker.Consumers;
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
        var order = new Order
        {
            Id = msg.Id,
            CustomerId = msg.CustomerId,
            CreatedAt = msg.CreatedAt,
            Status = (EnumStatus)msg.Status,
            DeliveryMethod = (EnumDeliveryMethod)msg.DeliveryMethod,
            Items = msg.Items.Select(i => new OrderItem
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

