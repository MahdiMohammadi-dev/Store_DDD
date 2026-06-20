using MediatR;
using Store.Domain.Entity.Products.Events;

namespace Store.Application.Products.Events;

public sealed class ProductCreatedDomainEventHandler
    : INotificationHandler<ProductCreatedDomainEvent>
{
    public Task Handle(
        ProductCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        // مثلا log
        Console.WriteLine($"Product Created: {notification.ProductId}");

        return Task.CompletedTask;
    }
}