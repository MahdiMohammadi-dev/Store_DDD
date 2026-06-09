using Microsoft.EntityFrameworkCore;
using Store.Domain.Entity.Customers;
using Store.Domain.Repositories;
using Store.Persistence.Context;

namespace Store.Persistence.Repositories;


public sealed class CustomerRepository : ICustomerRepository
{
    private readonly StoreDbContext _context;

    public CustomerRepository(StoreDbContext context) => _context = context;

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await _context.Customers.AddAsync(customer, cancellationToken);
    }
}