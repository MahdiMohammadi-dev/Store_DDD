using System.Text.Json;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Store.Persistence.Context;

namespace Store.Infrastructure
{
    public class OutboxProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMediator _mediator;

        public OutboxProcessor(
            IServiceScopeFactory scopeFactory,
            IMediator mediator)
        {
            _scopeFactory = scopeFactory;
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ProcessOutbox(stoppingToken);

                await Task.Delay(5000, stoppingToken);
            }
        }


        private async Task ProcessOutbox(CancellationToken ct)
        {
            using var scope = _scopeFactory.CreateScope();

            var db = scope.ServiceProvider
                .GetRequiredService<StoreDbContext>();

            var messages = db.OutboxMessages
                .Where(x => x.ProcessedOn == null)
                .Take(20)
                .ToList();

            foreach (var message in messages)
            {
                var type = Type.GetType(message.Type);

                var domainEvent =
                    JsonSerializer.Deserialize(message.Content, type!);

                await _mediator.Publish(domainEvent!, ct);

                message.ProcessedOn = DateTime.UtcNow;
            }

            await db.SaveChangesAsync(ct);
        }
    }
    }

