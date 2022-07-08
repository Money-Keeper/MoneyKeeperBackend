using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Facades.UserFacades.Abstractions;

internal interface IUserQueries
{
    Task<User?> GetAsync(Guid id);
}
