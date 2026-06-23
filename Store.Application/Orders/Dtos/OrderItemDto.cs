namespace Store.Application.Orders.Dtos;

public sealed record OrderItemDto(Guid ProductId, int Quantity, decimal UnitPrice);