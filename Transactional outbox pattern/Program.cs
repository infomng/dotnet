using Entities;
using Microsoft.EntityFrameworkCore;
using Transactional_outbox_pattern.src.Consumers;
using Transactional_outbox_pattern.src.Database;
using Transactional_outbox_pattern.src.Services.MessageBroker;
using Transactional_outbox_pattern.src.Services.Outbox;

var builder = WebApplication.CreateBuilder(args);
var Services = builder.Services;

Services.AddEndpointsApiExplorer();
Services.AddSwaggerGen();
Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TransactionalOutboox"));
Services.AddScoped<OrderService>();
Services.AddScoped<OutboxProcessor>();
Services.AddSingleton<IMessageBroker, MessageBroker>();
Services.AddScoped<OrderPlacedConsumer>();
Services.AddHostedService<OutboxProcessorBackgroundService>();

var app = builder.Build();

app.UseHttpsRedirection();

using var scope = app.Services.CreateScope();
var orderService = scope.ServiceProvider.GetRequiredService<OrderService>();

await orderService.PlaceOrderAsync(new Order
{
    Id = Guid.NewGuid(),
    Total = 99.99m,
    CreatedAt = DateTime.UtcNow
});


app.Run();
