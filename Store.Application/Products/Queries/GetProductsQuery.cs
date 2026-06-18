using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Products.Dtos;

namespace Store.Application.Products.Queries;

public sealed record GetProductsQuery():IRequest<List<ProductDto>>;


public class GetProductsQueryHandler:IRequestHandler<GetProductsQuery,List<ProductDto>>
{


    
    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        // return await _context.Products.Select(x => new ProductDto(x.Id, x.Name.Value, x.Price.Amount, x.Stock))
        //     .ToListAsync(cancellationToken);

        return new();

    }

}


