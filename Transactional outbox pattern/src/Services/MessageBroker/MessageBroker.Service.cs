
namespace Transactional_outbox_pattern.src.Services.MessageBroker;

public class MessageBroker : IMessageBroker
{
    public Task PublishAsync(string eventType, string payload)
    {
        Console.WriteLine($"Published: {eventType}, Payload: {payload}");

        return Task.CompletedTask;
    }
}