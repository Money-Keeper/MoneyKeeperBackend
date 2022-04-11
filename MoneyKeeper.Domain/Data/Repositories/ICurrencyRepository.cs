using MoneyKeeper.Domain.Data.Models;

namespace MoneyKeeper.Domain.Data.Repositories;

public interface ICurrencyRepository
{
    Task<Currency?> GetAsync(Guid id);
    Task<IEnumerable<Currency>> GetAsync();
    Task<bool> CreateAsync(Currency currency);
    Task<bool> UpdateAsync(Guid id, Currency currency);
    Task DeleteAsync(Guid id);
}
