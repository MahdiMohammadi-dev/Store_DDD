namespace Store.Domain.Entity.Orders;

public class OrderException  : Exception
{
    public OrderException(string? message) : base(message)
    {
    }
}