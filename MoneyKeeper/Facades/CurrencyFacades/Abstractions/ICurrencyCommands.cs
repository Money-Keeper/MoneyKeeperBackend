using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Facades.CurrencyFacades.Abstractions;

internal interface ICurrencyCommands
{
    Task<Currency> CreateAsync(Currency currency);
    Task<Currency> UpdateAsync(Guid id, Currency currency);
    Task DeleteAsync(Guid id);
}
