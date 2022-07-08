namespace MoneyKeeper.Infrastructure.UserContext;

internal sealed class UserContext : IUserContext
{
    private const string UserId = nameof(UserId);

    private static readonly HttpContextAccessor _accessor = new HttpContextAccessor();

    public bool IsAuthorized => _accessor.HttpContext?.Items[UserId] != null;
    public Guid? CurrentUserId => _accessor.HttpContext?.Items[UserId] as Guid?;
}
