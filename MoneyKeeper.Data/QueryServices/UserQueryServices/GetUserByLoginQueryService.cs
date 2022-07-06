using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.UserQueries;

namespace MoneyKeeper.Data.QueryServices.UserQueryServices;

public sealed class GetUserByLoginQueryService : IQueryService<GetUserByLoginQuery, User?>
{
    private readonly AppDbContext _dbContext;

    public GetUserByLoginQueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<User?> ExecuteAsync(GetUserByLoginQuery parameter)
    {
        return _dbContext.Users
            .AsNoTracking()
            .Where(x => x.DeletedAt == null)
            .FirstOrDefaultAsync(x => x.Login == parameter.Login);
    }
}
