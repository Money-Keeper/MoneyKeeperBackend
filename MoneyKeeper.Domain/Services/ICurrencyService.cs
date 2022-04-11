using MoneyKeeper.Domain.Data.Models;
using MoneyKeeper.Domain.Dtos;

namespace MoneyKeeper.Domain.Services;

public interface ICurrencyService
{
    Task<CurrencyDto?> GetAsync(Guid id);
    Task<DataResult<CurrencyDto>> GetAsync();
    Task<bool> CreateAsync(NewCurrencyDto currencyDto);
    Task<bool> UpdateAsync(Guid id, NewCurrencyDto currencyDto);
    Task DeleteAsync(Guid id);
}
