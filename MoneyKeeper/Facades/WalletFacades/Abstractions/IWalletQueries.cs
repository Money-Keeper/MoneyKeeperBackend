using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Facades.WalletFacades.Abstractions;

internal interface IWalletQueries
{
    Task<bool> ExistsAsync(Guid id);
    Task<Wallet?> GetAsync(Guid id, Guid userId);
    Task<IEnumerable<Wallet>> GetAsync(Guid userId);
}
