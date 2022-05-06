using MoneyKeeper.Domain.Commands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Data.CommandServices.ExpenseCommandServices;

public sealed class DeleteExpenseCommandService : ICommandService<DeleteEntityCommand<Expense>, EmptyCommandResult>
{
    private readonly AppDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public DeleteExpenseCommandService(AppDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public async Task<EmptyCommandResult> ExecuteAsync(DeleteEntityCommand<Expense> parameter)
    {
        Expense expense = _dbContext.Expenses.Attach(new Expense { Id = parameter.Id }).Entity;

        expense.DeletedAt = _dateTimeProvider.NowUtc;

        await _dbContext.SaveChangesAsync();

        return new EmptyCommandResult();
    }
}
