using MediatR;
using Store.Application.Products.Dtos;

namespace Store.Application.Products.Queries;

public sealed record GetProductsQuery():IRequest<List<ProductDto>>;


public class GetProductsQueryHandler:IRequestHandler<GetProductsQuery,List<ProductDto>>
{


    
    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {

        return new();

    }

}


