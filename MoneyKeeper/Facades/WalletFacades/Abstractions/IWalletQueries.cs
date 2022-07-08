using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Facades.WalletFacades.Abstractions;

internal interface IWalletQueries
{
    Task<bool> ExistsAsync(Guid id, Guid userId);
    Task<Wallet?> GetAsync(Guid id, Guid userId);
    Task<IEnumerable<Wallet>> GetAsync(Guid userId);
}
