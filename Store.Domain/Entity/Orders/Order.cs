using Store.Domain.Abstractions;
using Store.Domain.Enums;
using Store.Domain.ValueObjects;

namespace Store.Domain.Entity.Orders;

public class Order :BaseEntity
{
    private readonly List<OrderItem> _items = new List<OrderItem>();


    public Guid CustomerId { get; private set; }

    public OrderStatus Status { get; private set; }

    public IReadOnlyCollection<OrderItem> Items
        => _items.AsReadOnly();


    // Constructor
    private Order()
    {
    }

    public Order(Guid customerId)
    {
        Id = Guid.NewGuid();

        CustomerId = customerId;

        Status = OrderStatus.Pending;
    }


    // Behaviors


    public void AddItem(Guid productId, int quantity, Money unitPrice)
    {
        if (quantity <= 0)
            throw new OrderException(OrderErrors.InvalidQuantity);

        var item = _items.FirstOrDefault(x => x.ProductId == productId);

        if (item != null) throw new OrderException(OrderErrors.ProductAlreadyExists);

        _items.Add(new OrderItem(productId, quantity, unitPrice));
    }


    public void Pay()
    {
        if (!_items.Any()) throw new OrderException(OrderErrors.EmptyOrder);

        if (Status == OrderStatus.Paid) throw new OrderException(OrderErrors.OrderAlreadyPaid);
        
        if (Status == OrderStatus.Cancelled) throw new OrderException(OrderErrors.CancellationStatus);

        Status = OrderStatus.Paid;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Paid)
            throw new OrderException(OrderErrors.PaidStatus);

        Status = OrderStatus.Cancelled;
    }

}