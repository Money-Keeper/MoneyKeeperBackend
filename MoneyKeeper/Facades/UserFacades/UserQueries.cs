using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.UserQueries;
using MoneyKeeper.Facades.UserFacades.Abstractions;

namespace MoneyKeeper.Facades.UserFacades;

internal sealed class UserQueries : IUserQueries
{
    private readonly IQueryService<GetUserByIdQuery, User?> _getUserByIdService;

    public UserQueries(IQueryService<GetUserByIdQuery, User?> getUserByIdService)
    {
        _getUserByIdService = getUserByIdService ?? throw new ArgumentNullException(nameof(getUserByIdService));
    }

    public Task<User?> GetAsync(Guid id)
    {
        return _getUserByIdService.ExecuteAsync(new GetUserByIdQuery(id));
    }
}
