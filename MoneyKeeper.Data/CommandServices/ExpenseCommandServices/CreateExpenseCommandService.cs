using MoneyKeeper.Domain.Commands.ExpenseCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;

namespace MoneyKeeper.Data.CommandServices.ExpenseCommandServices;

public sealed class CreateExpenseCommandService : ICommandService<CreateExpenseCommand, CreateExpenseCommandResult>
{
    private readonly AppDbContext _dbContext;

    public CreateExpenseCommandService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<CreateExpenseCommandResult> ExecuteAsync(CreateExpenseCommand parameter)
    {
        Guid result = _dbContext.Expenses.Add(parameter.NewExpense).Entity.Id;

        await _dbContext.SaveChangesAsync();

        return new CreateExpenseCommandResult(result);
    }
}
