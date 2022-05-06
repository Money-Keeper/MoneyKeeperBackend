using MoneyKeeper.Domain.Infrastructure;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.ExpenseCommands;

public sealed class CreateExpenseCommandResult : ICommandResult, IDataResult<Guid?>
{
    public CreateExpenseCommandResult(Guid? data)
    {
        Data = data;
    }

    public Guid? Data { get; }
}
