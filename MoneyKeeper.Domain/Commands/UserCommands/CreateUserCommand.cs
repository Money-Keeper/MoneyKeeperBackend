using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.UserCommands;

public sealed class CreateUserCommand : ICommand<EmptyCommandResult>
{
    public CreateUserCommand(User newUser)
    {
        NewUser = newUser ?? throw new ArgumentNullException(nameof(newUser));
    }

    public User NewUser { get; }
}
