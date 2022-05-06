using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.CurrencyQueries;

namespace MoneyKeeper.Data.QueryServices.CurrencyQueryServices;

public sealed class GetCurrencyByIdQueryService : IQueryService<GetCurrencyByIdQuery, Currency?>
{
    private readonly AppDbContext _dbContext;

    public GetCurrencyByIdQueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<Currency?> ExecuteAsync(GetCurrencyByIdQuery parameter)
    {
        return _dbContext.Currencies
            .AsNoTracking()
            .Where(x => x.DeletedAt == null)
            .FirstOrDefaultAsync(x => x.Id == parameter.Id);
    }
}
