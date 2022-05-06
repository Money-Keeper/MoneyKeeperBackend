using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.ExpenseCommands;

public sealed class UpdateExpenseCommand : ICommand<UpdateExpenseCommandResult>
{
    public UpdateExpenseCommand(Guid id, Expense newExpense)
    {
        Id = id;
        NewExpense = newExpense ?? throw new ArgumentNullException(nameof(newExpense));
    }

    public Guid Id { get; }
    public Expense NewExpense { get; }
}
