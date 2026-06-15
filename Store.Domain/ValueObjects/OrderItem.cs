using Store.Domain.Abstractions;

namespace Store.Domain.ValueObjects;

public sealed class OrderItem
{
    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    public Money UnitPrice { get; private set; }

    private OrderItem()
    {
    }

    internal OrderItem(
        Guid productId,
        int quantity,
        Money unitPrice)
    {
        ProductId = productId;

        Quantity = quantity;

        UnitPrice = unitPrice;
    }
}