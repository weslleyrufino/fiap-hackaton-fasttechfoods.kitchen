using FastTechFoods.Contracts;
using FastTechFoods.Kitchen.Domain.Entities.Enum;
using FastTechFoods.Kitchen.Infrastructure.Repository;
using MassTransit;

namespace FastTechFoods.Kitchen.Worker.Consumers;
public class CancelOrderByCustomerConsumer : IConsumer<CancellationReasonEvent>
{
    private readonly ApplicationDbContext _dbContext;

    public CancelOrderByCustomerConsumer(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<CancellationReasonEvent> context)
    {
        var msg = context.Message;

        var orderFromDB = _dbContext.Order.FirstOrDefault(x => x.Id == msg.Id);

        if (orderFromDB != null)
        {
            orderFromDB.CancellationReason = msg.CancellationReason;
            orderFromDB.Status = EnumStatus.Canceled;
            _dbContext.Order.Update(orderFromDB);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"[Worker][CancelOrderByCustomerConsumer] Order updated. Order.Id: {orderFromDB.Id}");
        }
        else
            Console.WriteLine($"[Worker][CancelOrderByCustomerConsumer] Order doesn't not exist in kitchen DB. Order.Id: {msg.Id}");
        
    }
}