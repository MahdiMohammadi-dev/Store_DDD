using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Abstractions;
using Store.Application.Orders.Dtos;

namespace Store.Application.Orders.Queries;

public sealed record GetOrdersQuery():IRequest<List<OrderDto>>;


public class GetOrdersQueryHandler :IRequestHandler<GetOrdersQuery,List<OrderDto>>
{
    private readonly IApplicationDbContext _context;

    public GetOrdersQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<List<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Orders.Select(x => new OrderDto() {customerId = x.CustomerId}).ToListAsync(cancellationToken);
    }
}