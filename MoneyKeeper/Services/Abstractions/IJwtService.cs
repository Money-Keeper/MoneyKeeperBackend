namespace MoneyKeeper.Services.Abstractions;

internal interface IJwtService
{
    string GetToken(Guid userId, TimeSpan? lifetime = null);
    bool ValidateToken(string token, out Guid? userId);
}
