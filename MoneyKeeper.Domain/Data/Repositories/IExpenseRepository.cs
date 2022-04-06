using MoneyKeeper.Domain.Data.Models;

namespace MoneyKeeper.Domain.Data.Repositories;

public interface IExpenseRepository
{
    Task<Expense?> GetAsync(Guid id);
    Task<IEnumerable<Expense>> GetAsync();
    Task<bool> CreateAsync(Expense expense);
    Task<bool> UpdateAsync(Guid id, Expense expense);
    Task DeleteAsync(Guid id);
}
