using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Data.Models;
using MoneyKeeper.Domain.Data.Repositories;
using MoneyKeeper.Domain.Providers;

namespace MoneyKeeper.Data.Repositories;

public sealed class CurrencyRepository : ICurrencyRepository
{
    private readonly MoneyKeeperContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CurrencyRepository(MoneyKeeperContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public Task<Currency?> GetAsync(Guid id)
    {
        return _dbContext.Currencies
            .AsNoTracking()
            .Where(x => x.DeletedAt == null)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Currency>> GetAsync()
    {
        return await _dbContext.Currencies
            .AsNoTracking()
            .Where(x => x.DeletedAt == null)
            .ToListAsync();
    }

    public async Task<bool> CreateAsync(Currency currency)
    {
        _dbContext.Currencies.Add(currency);

        int affected = await _dbContext.SaveChangesAsync();

        return affected > 0;
    }

    public async Task<bool> UpdateAsync(Guid id, Currency currency)
    {
        currency.Id = id;
        currency.ModifiedAt = _dateTimeProvider.NowUtc;

        _dbContext.Currencies.Update(currency);

        int affected = await _dbContext.SaveChangesAsync();

        return affected > 0;
    }

    public async Task DeleteAsync(Guid id)
    {
        Currency currency = _dbContext.Currencies.Attach(new Currency { Id = id }).Entity;

        currency.DeletedAt = _dateTimeProvider.NowUtc;

        await _dbContext.SaveChangesAsync();
    }
}
