using MoneyKeeper.Domain.Infrastructure;
using MoneyKeeper.Domain.Infrastructure.Commands;

namespace MoneyKeeper.Domain.Commands.UserCommands;

public sealed class CreateUserCommandResult : ICommandResult, IDataResult<Guid?>
{
    public CreateUserCommandResult(Guid? data)
    {
        Data = data;
    }

    public Guid? Data { get; }
}
