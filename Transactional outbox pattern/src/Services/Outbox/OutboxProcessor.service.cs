using Microsoft.EntityFrameworkCore;
using Transactional_outbox_pattern.src.Database;
using Transactional_outbox_pattern.src.Services.MessageBroker;

namespace Transactional_outbox_pattern.src.Services.Outbox;

public class OutboxProcessor(AppDbContext dbContext, IMessageBroker messageBroker)
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IMessageBroker _messageBroker = messageBroker;

    public async Task ProcessOutboxAsync()
    {
        var unprocessedMessages = await _dbContext.Outbox
            .Where(o => !o.Processed)
            .ToListAsync();

        foreach (var message in unprocessedMessages)
        {
            try
            {
                // Publish to the broker
                await _messageBroker.PublishAsync(message.EventType, message.Payload);

                // Mark as processed
                // message.Processed = true;
                // _dbContext.Outbox.Update(message);
                // await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process message {message.Id}: {ex.Message}");
            }
        }
    }
}