namespace Transactional_outbox_pattern.src.Services.MessageBroker;

public interface IMessageBroker
{
    Task PublishAsync(string eventType, string payload);
}