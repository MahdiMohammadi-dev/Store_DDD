namespace Store.Domain.Entity.Orders;
public static class OrderErrors
{
    public const string InvalidQuantity = "Quantity is invalid";

    public const string ProductAlreadyExists = "Product already exists in order";

    public const string OrderAlreadyPaid = "Order already paid";

    public const string EmptyOrder = "Order contains no items";
    public const string CancellationStatus = "You can not pay because the order is cancelled";
}