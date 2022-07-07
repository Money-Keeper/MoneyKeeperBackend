namespace MoneyKeeper.Infrastructure.UserContext;

internal sealed class UserContext : IUserContext
{
    private const string Login = nameof(Login);

    private readonly IHttpContextAccessor _contextAccessor;

    public UserContext(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
    }

    public bool IsAuthorized => _contextAccessor.HttpContext?.Items[Login] != null;
    public string? CurrentUserLogin => _contextAccessor.HttpContext?.Items[Login] as string;
}
