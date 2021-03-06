using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Infrastructure.UserContext;

public interface IUserContext
{
    bool IsAuthorized { get; }
    Guid? CurrentUserId { get; }
}
