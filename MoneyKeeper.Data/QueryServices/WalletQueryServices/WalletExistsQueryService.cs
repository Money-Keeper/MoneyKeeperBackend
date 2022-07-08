using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Queries.WalletQueries;

namespace MoneyKeeper.Data.QueryServices.WalletQueryServices;

public sealed class WalletExistsQueryService : IQueryService<WalletExistsQuery, bool>
{
    private readonly AppDbContext _dbContext;

    public WalletExistsQueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<bool> ExecuteAsync(WalletExistsQuery parameter)
    {
        return _dbContext.Wallets
            .Where(x => x.DeletedAt == null)
            .AnyAsync(x => x.Id == parameter.Id && x.UserId == parameter.UserId);
    }
}
