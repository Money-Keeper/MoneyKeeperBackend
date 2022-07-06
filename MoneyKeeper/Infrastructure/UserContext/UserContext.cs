namespace MoneyKeeper.Infrastructure.UserContext;

internal sealed class UserContext : IUserContext
{
    private const string UserId = nameof(UserId);

    private readonly IHttpContextAccessor _contextAccessor;

    public UserContext(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
    }

    public Guid? CurrentUserId => _contextAccessor.HttpContext?.Items[UserId] as Guid?;
    public bool IsAuthorized => _contextAccessor.HttpContext?.Items[UserId] != null;

}
