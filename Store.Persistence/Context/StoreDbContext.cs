using Microsoft.EntityFrameworkCore;
using Store.Domain.Entity.Customers;
using Store.Domain.Entity.Orders;
using Store.Domain.Entity.Products;
using Store.Domain.Repositories;

namespace Store.Persistence.Context;

public class StoreDbContext : DbContext, IUnitOfWork
{
    public StoreDbContext(
        DbContextOptions<StoreDbContext> options)
        : base(options)
    {
    }

    protected StoreDbContext()
    {
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Customer> Customers => Set<Customer>();

}