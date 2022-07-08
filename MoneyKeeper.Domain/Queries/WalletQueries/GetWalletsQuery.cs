using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Queries.WalletQueries;

public sealed class GetWalletsQuery : IQuery<IEnumerable<Wallet>>
{
    public GetWalletsQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}
