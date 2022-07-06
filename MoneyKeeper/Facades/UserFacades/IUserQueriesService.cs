using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.UserFacades;

public interface IUserQueriesService
{
    Task<UserDto?> GetAsync(Guid id);
}
