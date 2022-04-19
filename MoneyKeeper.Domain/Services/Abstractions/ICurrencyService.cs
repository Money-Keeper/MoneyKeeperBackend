using MoneyKeeper.Domain.Dtos;

namespace MoneyKeeper.Domain.Services.Abstractions;

public interface ICurrencyService
{
    Task<bool> ExistsAsync(Guid id);
    Task<CurrencyDto?> GetAsync(Guid id);
    Task<DataResult<CurrencyDto>> GetAsync();
    Task<bool> CreateAsync(NewCurrencyDto currencyDto);
    Task<bool> UpdateAsync(Guid id, NewCurrencyDto currencyDto);
    Task DeleteAsync(Guid id);
}
