using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Facades.WalletFacades.Abstractions;

internal interface IWalletCommands
{
    Task<Wallet> CreateAsync(Wallet wallet, Guid userId);
    Task<Wallet> UpdateAsync(Guid id, Wallet wallet);
    Task DeleteAsync(Guid id);
}
