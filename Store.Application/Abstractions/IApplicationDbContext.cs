using Store.Domain.Entity.Products;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Entity.Orders;

namespace Store.Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Order> Orders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}