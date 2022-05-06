using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.CurrencyQueries;

namespace MoneyKeeper.Data.QueryServices.CurrencyQueryServices;

public sealed class GetCurrenciesQueryService : IQueryService<GetCurrenciesQuery, IEnumerable<Currency>>
{
    private readonly AppDbContext _dbContext;

    public GetCurrenciesQueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<Currency>> ExecuteAsync(GetCurrenciesQuery parameter)
    {
        return await _dbContext.Currencies
            .AsNoTracking()
            .Where(x => x.DeletedAt == null)
            .ToListAsync();
    }
}
