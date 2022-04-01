using MoneyKeeper.Domain.Data.Models;

namespace MoneyKeeper.Domain.Data.Repositories;

public interface IExpenseRepository
{
    Task<Expense?> Get(Guid id);
    Task<IEnumerable<Expense>> Get();
    Task<bool> CreateAsync(Expense expense);
    Task<bool> UpdateAsync(Guid id, Expense expense);
    Task DeleteAsync(Guid id);
}
