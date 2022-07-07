using MoneyKeeper.Domain.Models;
using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.ExpenseFacades.Abstractions;

internal interface IExpenseQueries
{
    Task<bool> ExistsAsync(Guid id);
    Task<Expense?> GetAsync(Guid id);
    Task<IEnumerable<Expense>> GetAsync(ExpenseConditionDto condition);
}
