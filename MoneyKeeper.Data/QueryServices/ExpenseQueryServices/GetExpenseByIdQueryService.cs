using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.ExpenseQueries;

namespace MoneyKeeper.Data.QueryServices.ExpenseQueryServices;

public sealed class GetExpenseByIdQueryService : IQueryService<GetExpenseByIdQuery, Expense?>
{
    private readonly AppDbContext _dbContext;

    public GetExpenseByIdQueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<Expense?> ExecuteAsync(GetExpenseByIdQuery parameter)
    {
        return _dbContext.Expenses
            .AsNoTracking()
            .Include(x => x.Currency)
            .Include(x => x.Category)
            .ThenInclude(x => x.ParentCategory)
            .Include(x => x.Invoice)
            .Where(x => x.DeletedAt == null)
            .FirstOrDefaultAsync(x => x.Id == parameter.Id);
    }
}
