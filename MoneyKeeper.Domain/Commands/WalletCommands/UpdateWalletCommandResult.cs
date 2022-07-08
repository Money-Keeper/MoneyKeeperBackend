using MoneyKeeper.Domain.Infrastructure;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.WalletCommands;

public sealed class UpdateWalletCommandResult : ICommandResult, IDataResult<Wallet>
{
    public UpdateWalletCommandResult(Wallet data)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public Wallet Data { get; }
}
