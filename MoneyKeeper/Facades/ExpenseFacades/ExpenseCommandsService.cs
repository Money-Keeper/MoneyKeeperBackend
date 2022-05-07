using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Commands;
using MoneyKeeper.Domain.Commands.ExpenseCommands;
using MoneyKeeper.Domain.Events;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Infrastructure.Events;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.ExpenseFacades;

public sealed class ExpenseCommandsService : IExpenseCommandsService
{
    private readonly IMapper _mapper;
    private readonly ICommandService<CreateExpenseCommand, CreateExpenseCommandResult> _createExpenseService;
    private readonly ICommandService<UpdateExpenseCommand, UpdateExpenseCommandResult> _updateExpenseService;
    private readonly ICommandService<DeleteEntityCommand<Expense>, EmptyCommandResult> _deleteExpenseService;
    private readonly IAsyncEventHandler<ExpenseUpdatedEvent> _expenseUpdatedHandler;

    public ExpenseCommandsService(
        IMapper mapper,
        ICommandService<CreateExpenseCommand, CreateExpenseCommandResult> createExpenseService,
        ICommandService<UpdateExpenseCommand, UpdateExpenseCommandResult> updateExpenseService,
        ICommandService<DeleteEntityCommand<Expense>, EmptyCommandResult> deleteExpenseService,
        IAsyncEventHandler<ExpenseUpdatedEvent> expenseUpdatedHandler)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _createExpenseService = createExpenseService ?? throw new ArgumentNullException(nameof(createExpenseService));
        _updateExpenseService = updateExpenseService ?? throw new ArgumentNullException(nameof(updateExpenseService));
        _deleteExpenseService = deleteExpenseService ?? throw new ArgumentNullException(nameof(deleteExpenseService));
        _expenseUpdatedHandler = expenseUpdatedHandler ?? throw new ArgumentNullException(nameof(expenseUpdatedHandler));
    }

    public async Task<Guid?> CreateAsync(NewExpenseDto newExpense)
    {
        Expense expense = _mapper.Map<NewExpenseDto, Expense>(newExpense)!;

        CreateExpenseCommandResult result = await _createExpenseService.ExecuteAsync(new CreateExpenseCommand(expense));

        return result.Data;
    }

    public Task DeleteAsync(Guid id)
    {
        return _deleteExpenseService.ExecuteAsync(new DeleteEntityCommand<Expense>(id));
    }

    public async Task<Guid?> UpdateAsync(Guid id, NewExpenseDto newExpense)
    {
        Expense expense = _mapper.Map<NewExpenseDto, Expense>(newExpense)!;

        UpdateExpenseCommandResult result = await _updateExpenseService.ExecuteAsync(new UpdateExpenseCommand(id, expense));

        if (result.Data.HasValue)
        {
            var e = new ExpenseUpdatedEvent(result.Data.Value, result.OldImageLink, result.OldPdfLink);

            await _expenseUpdatedHandler.HandleAsync(e);
        }

        return result.Data;
    }
}
