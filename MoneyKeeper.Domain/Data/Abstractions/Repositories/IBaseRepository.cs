using MoneyKeeper.Domain.Data.Models;

namespace MoneyKeeper.Domain.Data.Abstractions.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseModel
{
    Task<TEntity?> GetAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAsync();
    Task<bool> CreateAsync(TEntity entity);
    Task<bool> UpdateAsync(Guid id, TEntity entity);
    Task DeleteAsync(Guid id);
}
