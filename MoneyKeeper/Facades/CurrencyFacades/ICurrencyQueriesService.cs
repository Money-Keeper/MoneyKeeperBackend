using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.CurrencyFacades;

public interface ICurrencyQueriesService
{
    Task<bool> ExistsAsync(Guid id);
    Task<CurrencyDto?> GetAsync(Guid id);
    Task<DataResult<CurrencyDto>> GetAsync();
}
