using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Facades.CurrencyFacades.Abstractions;

internal interface ICurrencyQueries
{
    Task<bool> ExistsAsync(Guid id);
    Task<Currency?> GetAsync(Guid id);
    Task<IEnumerable<Currency>> GetAsync();
}
