using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Store.Persistence.Context;

public class StoreDbContextFactory : IDesignTimeDbContextFactory<StoreDbContext>
{
    private readonly IConfiguration _configuration;

    public StoreDbContextFactory(IConfiguration configuration) => _configuration = configuration;

    public StoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();

        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);

        return new StoreDbContext(optionsBuilder.Options);
    }
}