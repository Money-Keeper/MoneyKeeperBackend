using MoneyKeeper.Domain.Infrastructure;
using MoneyKeeper.Domain.Infrastructure.Commands;

namespace MoneyKeeper.Domain.Commands.ExpenseCommands;

public sealed class CreateExpenseCommandResult : ICommandResult, IDataResult<Guid>
{
    public CreateExpenseCommandResult(Guid data)
    {
        Data = data;
    }

    public Guid Data { get; }
}
