using Store.Domain.Entity.Products;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Store.Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}