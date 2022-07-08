using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.WalletCommands;

public sealed class UpdateWalletCommand : ICommand<UpdateWalletCommandResult>
{
    public UpdateWalletCommand(Guid id, Wallet newWallet)
    {
        Id = id;
        NewWallet = newWallet ?? throw new ArgumentNullException(nameof(newWallet));
    }

    public Guid Id { get; }
    public Wallet NewWallet { get; }
}
