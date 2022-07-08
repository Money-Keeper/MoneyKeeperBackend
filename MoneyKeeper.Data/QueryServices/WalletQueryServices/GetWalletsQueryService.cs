using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.WalletQueries;

namespace MoneyKeeper.Data.QueryServices.WalletQueryServices;

public sealed class GetWalletsQueryService : IQueryService<GetWalletsQuery, IEnumerable<Wallet>>
{
    private readonly AppDbContext _dbContext;

    public GetWalletsQueryService(AppDbContext dbCotnext)
    {
        _dbContext = dbCotnext ?? throw new ArgumentNullException(nameof(dbCotnext));
    }

    public async Task<IEnumerable<Wallet>> ExecuteAsync(GetWalletsQuery parameter)
    {
        return await _dbContext.Wallets
            .AsNoTracking()
            .Where(x => x.DeletedAt == null && x.UserId == parameter.UserId)
            .ToListAsync();
    }
}
