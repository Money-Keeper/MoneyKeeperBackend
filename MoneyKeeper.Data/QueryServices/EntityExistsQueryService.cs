using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries;

namespace MoneyKeeper.Data.QueryServices;

public sealed class EntityExistsQueryService<TEntity> : IQueryService<EntityExistsQuery<TEntity>, bool> where TEntity : BaseModel
{
    private readonly AppDbContext _dbContext;

    public EntityExistsQueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<bool> ExecuteAsync(EntityExistsQuery<TEntity> parameter)
    {
        return _dbContext.EntityExistsAsync<TEntity>(parameter.Id);
    }
}
