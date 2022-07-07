using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Queries.UserQueries;

namespace MoneyKeeper.Data.QueryServices.UserQueryServices;

public sealed class UserExistsByLoginQueryService : IQueryService<UserExistsByLoginQuery, bool>
{
    private readonly AppDbContext _dbContext;

    public UserExistsByLoginQueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<bool> ExecuteAsync(UserExistsByLoginQuery parameter)
    {
        return _dbContext.Users.AnyAsync(x => x.Login == parameter.Login);
    }
}
