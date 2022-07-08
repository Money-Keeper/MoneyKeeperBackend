using MoneyKeeper.Domain.Infrastructure;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.UserCommands;

public sealed class CreateUserCommandResult : ICommandResult, IDataResult<User>
{
    public CreateUserCommandResult(User data)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public User Data { get; }
}
