using MoneyKeeper.Domain.Infrastructure.Queries;

namespace MoneyKeeper.Domain.Queries.UserQueries;

public sealed class UserExistsQuery : IQuery<bool>
{
    public UserExistsQuery(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentNullException(nameof(login));

        Login = login;
    }

    public string Login { get; }
}
