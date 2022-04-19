using MoneyKeeper.Domain.Data.Abstractions;
using MoneyKeeper.Domain.Data.Models;

namespace MoneyKeeper.Data;

public sealed class EntityHelper : IEntityHelper
{
    private readonly MoneyKeeperContext _dbContext;

    public EntityHelper(MoneyKeeperContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<bool> ExistsAsync<TEntity>(Guid id) where TEntity : BaseModel
    {
        return _dbContext.EntityExistsAsync<TEntity>(id);
    }
}
