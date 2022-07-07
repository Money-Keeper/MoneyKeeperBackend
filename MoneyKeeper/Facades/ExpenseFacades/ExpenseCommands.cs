using MoneyKeeper.Domain.Commands;
using MoneyKeeper.Domain.Commands.ExpenseCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Facades.ExpenseFacades.Abstractions;

namespace MoneyKeeper.Facades.ExpenseFacades;

internal sealed class ExpenseCommands : IExpenseCommands
{
    private readonly ICommandService<CreateExpenseCommand, CreateExpenseCommandResult> _createExpenseService;
    private readonly ICommandService<UpdateExpenseCommand, UpdateExpenseCommandResult> _updateExpenseService;
    private readonly ICommandService<DeleteEntityCommand<Expense>, EmptyCommandResult> _deleteExpenseService;

    public ExpenseCommands(
        ICommandService<CreateExpenseCommand, CreateExpenseCommandResult> createExpenseService,
        ICommandService<UpdateExpenseCommand, UpdateExpenseCommandResult> updateExpenseService,
        ICommandService<DeleteEntityCommand<Expense>, EmptyCommandResult> deleteExpenseService)
    {
        _createExpenseService = createExpenseService ?? throw new ArgumentNullException(nameof(createExpenseService));
        _updateExpenseService = updateExpenseService ?? throw new ArgumentNullException(nameof(updateExpenseService));
        _deleteExpenseService = deleteExpenseService ?? throw new ArgumentNullException(nameof(deleteExpenseService));
    }

    public async Task<Guid> CreateAsync(Expense expense)
    {
        return (await _createExpenseService.ExecuteAsync(new CreateExpenseCommand(expense))).Data;
    }

    public Task DeleteAsync(Guid id)
    {
        return _deleteExpenseService.ExecuteAsync(new DeleteEntityCommand<Expense>(id));
    }

    public Task<UpdateExpenseCommandResult> UpdateAsync(Guid id, Expense expense)
    {
        return _updateExpenseService.ExecuteAsync(new UpdateExpenseCommand(id, expense));
    }
}
