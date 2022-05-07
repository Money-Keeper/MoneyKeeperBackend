using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.ExpenseQueries;

namespace MoneyKeeper.Data.QueryServices.ExpenseQueryServices;

public sealed class GetExpensesByConditionQueryService : IQueryService<GetExpensesByConditionQuery, IEnumerable<Expense>>
{
    private readonly AppDbContext _dbContext;

    public GetExpensesByConditionQueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<Expense>> ExecuteAsync(GetExpensesByConditionQuery parameter)
    {
        var query = _dbContext.Expenses
            .AsNoTracking()
            .Include(x => x.Currency)
            .Include(x => x.Category)
            .ThenInclude(x => x.ParentCategory)
            .Include(x => x.Invoice)
            .Where(x => x.DeletedAt == null && x.CategoryId == parameter.CategoryId);

        if (parameter.DateFrom != null && parameter.DateTo != null)
        {
            query = query.Where(x => x.Date >= parameter.DateFrom && x.Date <= parameter.DateTo);
        }

        return await query.ToListAsync();
    }
}
