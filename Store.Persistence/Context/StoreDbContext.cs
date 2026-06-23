using Microsoft.EntityFrameworkCore;
using Store.Application.Abstractions;
using Store.Domain.Abstractions;
using Store.Domain.DomainEvents;
using Store.Domain.Entity.Customers;
using Store.Domain.Entity.Orders;
using Store.Domain.Entity.Products;
using Store.Domain.Repositories;
using System.Text.Json;

namespace Store.Persistence.Context;

public class StoreDbContext : DbContext, IUnitOfWork, IStoreDbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Customer> Customers => Set<Customer>();
    
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(StoreDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {

        var result = await base.SaveChangesAsync(cancellationToken);
        
         ConvertDomainEventsToOutbox();

        return result;

    }


    private void ConvertDomainEventsToOutbox()
    {
        var domainEvents = ChangeTracker
            .Entries<AggregateRoot>()
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = domainEvent.GetType().Name,
                Content = JsonSerializer.Serialize(domainEvent),
                OccurredOn = DateTime.UtcNow
            };

            OutboxMessages.Add(outboxMessage);
        }

        foreach (var entity in ChangeTracker
                     .Entries<AggregateRoot>())
        {
            entity.Entity.ClearDomainEvents();
        }
    }
}