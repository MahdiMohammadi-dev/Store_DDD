using Store.Domain.Entity.Customers;
using Store.Domain.Entity.Orders;
using Store.Domain.Entity.Products;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Store.Application.Abstractions;

public interface IStoreDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Order> Orders { get; }
    DbSet<Customer> Customers { get; }
}