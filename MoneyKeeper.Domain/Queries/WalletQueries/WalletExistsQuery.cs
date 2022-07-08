using MoneyKeeper.Domain.Infrastructure.Queries;

namespace MoneyKeeper.Domain.Queries.WalletQueries;

public sealed class WalletExistsQuery : IQuery<bool>
{
    public WalletExistsQuery(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
}
