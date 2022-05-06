using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.ExpenseCommands;

public sealed class CreateExpenseCommand : ICommand<CreateExpenseCommandResult>
{
    public CreateExpenseCommand(Expense newExpense)
    {
        NewExpense = newExpense ?? throw new ArgumentNullException(nameof(newExpense));
    }

    public Expense NewExpense { get; }
}
