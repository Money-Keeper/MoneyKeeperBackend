using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Data.Abstractions.Repositories;
using MoneyKeeper.Domain.Data.Models;
using MoneyKeeper.Domain.Providers;

namespace MoneyKeeper.Data.Repositories;

public sealed class ExpenseRepository : IExpenseRepository
{
    private readonly MoneyKeeperContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ExpenseRepository(MoneyKeeperContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public Task<Expense?> GetAsync(Guid id)
    {
        return _dbContext.Expenses
            .AsNoTracking()
            .Include(x => x.Currency)
            .Include(x => x.Category)
            .ThenInclude(x => x.ParentCategory)
            .Where(x => x.DeletedAt == null)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Expense>> GetAsync()
    {
        return await _dbContext.Expenses
            .AsNoTracking()
            .Include(x => x.Currency)
            .Include(x => x.Category)
            .ThenInclude(x => x.ParentCategory)
            .Where(x => x.DeletedAt == null)
            .ToListAsync();
    }

    public async Task<bool> CreateAsync(Expense expense)
    {
        bool isNestedEntitiesExists = await IsNestedEntitiesExistsAsync(expense);

        if (!isNestedEntitiesExists)
            return false;

        expense.Date = expense.Date.ToUniversalTime();

        _dbContext.Expenses.Add(expense);

        int affected = await _dbContext.SaveChangesAsync();

        return affected > 0;
    }

    public async Task<bool> UpdateAsync(Guid id, Expense expense)
    {
        bool isNestedEntitiesExists = await IsNestedEntitiesExistsAsync(expense);

        if (!isNestedEntitiesExists)
            return false;

        expense.Id = id;
        expense.Date = expense.Date.ToUniversalTime();
        expense.ModifiedAt = _dateTimeProvider.NowUtc;

        _dbContext.Update(expense);

        int affected = await _dbContext.SaveChangesAsync();

        return affected > 0;
    }

    public async Task DeleteAsync(Guid id)
    {
        Expense expense = _dbContext.Expenses.Attach(new Expense { Id = id }).Entity;

        expense.DeletedAt = _dateTimeProvider.NowUtc;

        await _dbContext.SaveChangesAsync();
    }

    private async Task<bool> IsNestedEntitiesExistsAsync(Expense expense)
    {
        bool isCurrencyExists = await _dbContext.IsEntityExistsAsync<Currency>(expense.CurrencyId);
        bool isCategoryExists = await _dbContext.IsEntityExistsAsync<Category>(expense.CategoryId);

        return isCurrencyExists
            && isCategoryExists;
    }
}
