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

        // Use a connection string for design-time (e.g., from configuration, environment variable, or hard-coded)
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);

        return new StoreDbContext(optionsBuilder.Options);
    }
}