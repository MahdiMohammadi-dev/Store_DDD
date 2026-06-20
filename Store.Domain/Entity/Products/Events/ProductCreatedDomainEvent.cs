using Store.Domain.DomainEvents;

namespace Store.Domain.Entity.Products.Events;

public record ProductCreatedDomainEvent(Guid Id, Guid ProductId) : IDomainEvent;
