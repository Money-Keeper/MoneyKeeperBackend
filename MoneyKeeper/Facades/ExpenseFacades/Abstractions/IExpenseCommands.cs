using MoneyKeeper.Domain.Commands.ExpenseCommands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Facades.ExpenseFacades.Abstractions;

internal interface IExpenseCommands
{
    Task<Guid> CreateAsync(Expense expense);
    Task<UpdateExpenseCommandResult> UpdateAsync(Guid id, Expense expense);
    Task DeleteAsync(Guid id);
}
