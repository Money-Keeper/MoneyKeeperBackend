using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.WalletCommands;

public sealed class CreateWalletCommand : ICommand<CreateWalletCommandResult>
{
    public CreateWalletCommand(Wallet newWallet)
    {
        NewWallet = newWallet ?? throw new ArgumentNullException(nameof(newWallet));
    }

    public Wallet NewWallet { get; }
}
