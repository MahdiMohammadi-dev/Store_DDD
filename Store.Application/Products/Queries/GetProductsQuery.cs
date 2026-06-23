using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Abstractions;
using Store.Application.Products.Dtos;

namespace Store.Application.Products.Queries;

public sealed record GetProductsQuery():IRequest<List<ProductDto>>;


public class GetProductsQueryHandler:IRequestHandler<GetProductsQuery,List<ProductDto>>
{
    private readonly IStoreDbContext _context;

    public GetProductsQueryHandler(IStoreDbContext context) => _context = context;

    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {

        return await _context.Products.AsNoTracking()
            .Select(x => new ProductDto(x.Id, x.Name.Value, x.Price.Amount, x.Stock)).ToListAsync(cancellationToken);

    }

}


