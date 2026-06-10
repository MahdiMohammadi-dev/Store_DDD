using Store.Domain.Events;

namespace Store.Domain.Entity.Orders.Events;

public sealed record OrderPaidDomainEvent(Guid OrderId):IDomainEvent;