using System.Text.Json;
using Entities;
using Transactional_outbox_pattern.src.Database;


public class OrderService(AppDbContext dbContext)
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task PlaceOrderAsync(Order order)
    {
        // using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            var outboxEntry = new Outbox
            {
                Id = Guid.NewGuid(),
                EventType = "OrderPlaced",
                Payload = JsonSerializer.Serialize(new { order.Id, order.Total }),
                CreatedAt = DateTime.UtcNow
            };

            await _dbContext.AddAsync(outboxEntry);
            await _dbContext.SaveChangesAsync();

            Console.WriteLine(outboxEntry);

            // await transaction.CommitAsync();
        }
        catch
        {
            // await transaction.RollbackAsync();
            throw;
        }
    }
}