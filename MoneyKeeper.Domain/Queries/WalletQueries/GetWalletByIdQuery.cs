using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Queries.WalletQueries;

public sealed class GetWalletByIdQuery : IQuery<Wallet?>
{
    public GetWalletByIdQuery(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
}
