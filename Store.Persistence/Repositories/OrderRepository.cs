using Microsoft.EntityFrameworkCore;
using Store.Domain.Entity.Orders;
using Store.Domain.Repositories;
using Store.Persistence.Context;

namespace Store.Persistence.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly StoreDbContext _context;

    public OrderRepository(StoreDbContext context) => _context = context;


    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
    }
}