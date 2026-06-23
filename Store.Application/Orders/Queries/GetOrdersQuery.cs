using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Abstractions;
using Store.Application.Orders.Dtos;
using Store.Domain.Repositories;

namespace Store.Application.Orders.Queries;

public sealed record GetOrdersQuery() : IRequest<List<OrderDto>>;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderDto>>
{
    private readonly IStoreDbContext _context;

    public GetOrdersQueryHandler(IStoreDbContext context) => _context = context;

    public async Task<List<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .AsNoTracking()
            .Include(o => o.Items)   
            .ToListAsync(cancellationToken);

        return orders
            .Select(o => new OrderDto(
                o.Id,
                o.CustomerId,
                o.Status.ToString(),
                o.Items.Sum(i => i.UnitPrice.Amount * i.Quantity),  
                o.Items
                    .Select(i => new OrderItemDto(i.ProductId, i.Quantity, i.UnitPrice.Amount))
                    .ToList()))
            .ToList();
    }
}