using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.CurrencyFacades;

public interface ICurrencyCommandsService
{
    Task<CurrencyDto?> CreateAsync(NewCurrencyDto newCurrency);
    Task<CurrencyDto?> UpdateAsync(Guid id, NewCurrencyDto newCurrency);
    Task DeleteAsync(Guid id);
}
