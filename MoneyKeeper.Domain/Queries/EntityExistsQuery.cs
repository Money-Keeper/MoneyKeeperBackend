using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Queries;

public sealed class EntityExistsQuery<TEntity> : IQuery<bool> where TEntity : BaseModel
{
    public EntityExistsQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
