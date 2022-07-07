namespace MoneyKeeper.Services.Abstractions;

internal interface IJwtService
{
    string GetToken(string login, TimeSpan? lifetime = null);
    bool ValidateToken(string token, out string? login);
}
