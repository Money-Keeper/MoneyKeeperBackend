using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.WalletCommands;

public sealed class CreateWalletCommand : ICommand<CreateWalletCommandResult>
{
    public CreateWalletCommand(Wallet newWallet, Guid userId)
    {
        NewWallet = newWallet ?? throw new ArgumentNullException(nameof(newWallet));
        UserId = userId;
    }

    public Wallet NewWallet { get; }
    public Guid UserId { get; }
}
