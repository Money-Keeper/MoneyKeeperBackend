using MoneyKeeper.Domain.Infrastructure.Queries;

namespace MoneyKeeper.Domain.Queries.UserQueries;

public sealed class UserExistsByLoginQuery : IQuery<bool>
{
    public UserExistsByLoginQuery(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentNullException(nameof(login));

        Login = login;
    }

    public string Login { get; }
}
