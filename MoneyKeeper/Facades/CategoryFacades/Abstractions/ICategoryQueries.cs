using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Facades.CategoryFacades.Abstractions;

internal interface ICategoryQueries
{
    Task<bool> ExistsAsync(Guid id);
    Task<Category?> GetAsync(Guid id);
    Task<IEnumerable<Category>> GetAsync();
}
