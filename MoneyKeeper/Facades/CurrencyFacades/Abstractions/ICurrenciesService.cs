using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.CurrencyFacades.Abstractions;

public interface ICurrenciesService
{
    Task<CurrencyDto> CreateAsync(NewCurrencyDto dto);
    Task<CurrencyDto> UpdateAsync(Guid id, NewCurrencyDto dto);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<CurrencyDto?> GetAsync(Guid id);
    Task<DataResult<CurrencyDto>> GetAsync();
}
