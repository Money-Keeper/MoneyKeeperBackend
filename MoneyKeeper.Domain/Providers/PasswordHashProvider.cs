using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Domain.Providers;

public sealed class PasswordHashProvider : IPasswordHashProvider
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
