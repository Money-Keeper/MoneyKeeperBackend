using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Queries.UserQueries;

public sealed class GetUserByLoginQuery : IQuery<User?>
{
    public GetUserByLoginQuery(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentNullException(nameof(login));

        Login = login;
    }

    public string Login { get; }
}
