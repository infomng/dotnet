using System.Text.Json;

namespace Transactional_outbox_pattern.src.Consumers;

public class OrderPlacedConsumer
{
    public Task HandleAsync(string payload)
    {
        var orderEvent = JsonSerializer.Deserialize<OrderPlacedEvent>(payload);
        Console.WriteLine($"Order Received: ID={orderEvent?.Id}, Total={orderEvent?.Total}");
        return Task.CompletedTask;
    }
}

public record OrderPlacedEvent(Guid Id, decimal Total);