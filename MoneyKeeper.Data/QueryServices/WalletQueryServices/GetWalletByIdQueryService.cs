using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.WalletQueries;

namespace MoneyKeeper.Data.QueryServices.WalletQueryServices;

public sealed class GetWalletByIdQueryService : IQueryService<GetWalletByIdQuery, Wallet?>
{
    private readonly AppDbContext _dbContext;

    public GetWalletByIdQueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<Wallet?> ExecuteAsync(GetWalletByIdQuery parameter)
    {
        return _dbContext.Wallets
            .AsNoTracking()
            .Where(x => x.DeletedAt == null && x.UserId == parameter.UserId)
            .FirstOrDefaultAsync(x => x.Id == parameter.Id);
    }
}
