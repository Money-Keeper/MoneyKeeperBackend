using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.CategoryQueries;

namespace MoneyKeeper.Data.QueryServices.CategoryQueryServices;

public sealed class GetCategoryByIdQueryService : IQueryService<GetCategoryByIdQuery, Category?>
{
    private readonly AppDbContext _dbContext;

    public GetCategoryByIdQueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<Category?> ExecuteAsync(GetCategoryByIdQuery parameter)
    {
        return _dbContext.Categories
            .AsNoTracking()
            .Include(x => x.ParentCategory)
            .Where(x => x.DeletedAt == null)
            .FirstOrDefaultAsync(x => x.Id == parameter.Id);
    }
}
