using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Queries.UserQueries;

public sealed class GetUserByIdQuery : IQuery<User?>
{
    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
