namespace MoneyKeeper.Infrastructure.UserContext;

public interface IUserContext
{
    Guid? CurrentUserId { get; }
    bool IsAuthorized { get; }
}
