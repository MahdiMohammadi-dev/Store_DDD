using Store.Domain.Repositories;
using Store.Persistence.Context;

namespace Store.Persistence.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly StoreDbContext _dbContext;

    public UnitOfWork(StoreDbContext dbContext) => _dbContext = dbContext;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
}