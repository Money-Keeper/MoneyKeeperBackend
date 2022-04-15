using MoneyKeeper.Domain.Data.Models;

namespace MoneyKeeper.Domain.Data.Abstractions;

public interface IEntityHelper
{
    Task<bool> IsExistsAsync<TEntity>(Guid id) where TEntity : BaseModel;
}
