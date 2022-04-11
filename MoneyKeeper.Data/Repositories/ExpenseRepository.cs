using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Data.Models;
using MoneyKeeper.Domain.Data.Repositories;
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
            .Where(x => x.DeletedAt == null)
            .Include(x => x.Currency)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Expense>> GetAsync()
    {
        return await _dbContext.Expenses
            .Where(x => x.DeletedAt == null)
            .Include(x => x.Currency)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> CreateAsync(Expense expense)
    {
        Currency? existingCurrency = await _dbContext.Currencies
            .Where(x => x.DeletedAt == null)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == expense.CurrencyId);

        if (existingCurrency is null)
            return false;

        expense.Date = expense.Date.ToUniversalTime();

        _dbContext.Expenses.Add(expense);

        int affected = await _dbContext.SaveChangesAsync();

        return affected > 0;
    }

    public async Task<bool> UpdateAsync(Guid id, Expense expense)
    {
        Currency? existingCurrency = await _dbContext.Currencies
            .Where(x => x.DeletedAt == null)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == expense.CurrencyId);

        if (existingCurrency is null)
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
}
