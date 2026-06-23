namespace Store.Application.Orders.Dtos;

public sealed record OrderDto(
    Guid Id,
    Guid CustomerId,
    string Status,
    decimal TotalAmount,
    List<OrderItemDto> Items);