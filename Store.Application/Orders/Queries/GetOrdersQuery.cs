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
        return await _context.Orders
            .AsNoTracking()
            .Select(x => new OrderDto(
                x.Id,
                x.CustomerId,
                x.Status.ToString(),
                x.Items.Sum(i => i.UnitPrice.Amount * i.Quantity),
                x.Items.Select(a =>
                    new OrderItemDto(a.ProductId,
                        a.Quantity,
                        a.UnitPrice.Amount)).ToList()
            )).ToListAsync(cancellationToken);
    }
}