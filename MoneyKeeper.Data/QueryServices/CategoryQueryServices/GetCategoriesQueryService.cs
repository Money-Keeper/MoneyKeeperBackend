using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.CategoryQueries;

namespace MoneyKeeper.Data.QueryServices.CategoryQueryServices;

public sealed class GetCategoriesQueryService : IQueryService<GetCategoriesQuery, IEnumerable<Category>>
{
    private readonly AppDbContext _dbContext;

    public GetCategoriesQueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<Category>> ExecuteAsync(GetCategoriesQuery parameter)
    {
        return await _dbContext.Categories
            .AsNoTracking()
            .Include(x => x.ParentCategory)
            .Where(x => x.DeletedAt == null)
            .ToListAsync();
    }
}
