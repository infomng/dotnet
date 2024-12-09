namespace Transactional_outbox_pattern.src.Services.Outbox;


public class OutboxProcessorBackgroundService(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("Backgound service called");
            using var scope = _serviceProvider.CreateScope();
            OutboxProcessor outboxProcessor = scope.ServiceProvider.GetRequiredService<OutboxProcessor>();

            await outboxProcessor.ProcessOutboxAsync();

            await Task.Delay(5000, stoppingToken); // Process every 5 seconds
        }
    }
}