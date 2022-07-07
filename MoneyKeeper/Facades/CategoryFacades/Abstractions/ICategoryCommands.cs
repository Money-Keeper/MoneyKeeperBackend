using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Facades.CategoryFacades.Abstractions;

internal interface ICategoryCommands
{
    Task<Category> CreateAsync(Category category);
    Task<Category> UpdateAsync(Guid id, Category category);
    Task DeleteAsync(Guid id);
}
