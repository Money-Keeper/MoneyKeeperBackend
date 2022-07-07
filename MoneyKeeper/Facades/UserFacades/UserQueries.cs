using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.UserQueries;
using MoneyKeeper.Facades.UserFacades.Abstractions;

namespace MoneyKeeper.Facades.UserFacades;

internal sealed class UserQueries : IUserQueries
{
    private readonly IQueryService<GetUserByLoginQuery, User?> _getUserByLoginService;

    public UserQueries(IQueryService<GetUserByLoginQuery, User?> getUserByLoginService)
    {
        _getUserByLoginService = getUserByLoginService ?? throw new ArgumentNullException(nameof(getUserByLoginService));
    }

    public Task<User?> GetAsync(string login)
    {
        return _getUserByLoginService.ExecuteAsync(new GetUserByLoginQuery(login));
    }
}
