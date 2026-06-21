using MediatR;
using Store.Application.Orders.Dtos;
using Store.Domain.Repositories;

namespace Store.Application.Orders.Queries;

public sealed record GetOrdersQuery():IRequest<List<OrderDto>>;


public class GetOrdersQueryHandler :IRequestHandler<GetOrdersQuery,List<OrderDto>>
{
    


    public async Task<List<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
   
       return new();
    }
}