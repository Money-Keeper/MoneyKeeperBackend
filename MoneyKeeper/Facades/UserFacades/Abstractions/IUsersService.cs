using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.UserFacades.Abstractions;

public interface IUsersService
{
    Task<UserDto?> GetAsync(string login);
}
