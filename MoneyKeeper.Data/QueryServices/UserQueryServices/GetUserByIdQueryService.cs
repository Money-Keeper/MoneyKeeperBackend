using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.UserQueries;

namespace MoneyKeeper.Data.QueryServices.UserQueryServices;

public sealed class GetUserByIdQueryService : IQueryService<GetUserByIdQuery, User?>
{
    private readonly AppDbContext _dbContext;

    public GetUserByIdQueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<User?> ExecuteAsync(GetUserByIdQuery parameter)
    {
        return _dbContext.Users
            .AsNoTracking()
            .Where(x => x.DeletedAt == null)
            .FirstOrDefaultAsync(x => x.Id == parameter.Id);
    }
}
