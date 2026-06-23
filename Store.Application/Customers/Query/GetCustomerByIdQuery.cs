using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Abstractions;
using Store.Application.Customers.Dto;

namespace Store.Application.Customers.Query;

public sealed record GetCustomerByIdQuery(Guid CustomerId) : IRequest<CustomerDto?>;


public class GetCustomerByIdQueryHandler:IRequestHandler<GetCustomerByIdQuery,CustomerDto?>
{
    private readonly IStoreDbContext _context;

    public GetCustomerByIdQueryHandler(IStoreDbContext context) => _context = context;

    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Customers.AsNoTracking()
            .Where(x => x.Id == request.CustomerId)
            .Select(x => new CustomerDto(x.Id, x.Email.Value, x.Address.City, x.Address.Street, x.Address.PostalCode)).FirstOrDefaultAsync(cancellationToken);
    }
}