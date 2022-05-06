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
    private readonly ICommandService<CreateExpenseCommand, CreateExpenseCommandResult> _createExpenseCommand;
    private readonly ICommandService<UpdateExpenseCommand, UpdateExpenseCommandResult> _updateExpenseCommand;
    private readonly ICommandService<DeleteEntityCommand<Expense>, EmptyCommandResult> _deleteExpenseCommand;
    private readonly IAsyncEventHandler<ExpenseUpdatedEvent, EmptyEventResult> _expenseUpdatedEvent;

    public ExpenseCommandsService(
        IMapper mapper,
        ICommandService<CreateExpenseCommand, CreateExpenseCommandResult> createExpenseCommand,
        ICommandService<UpdateExpenseCommand, UpdateExpenseCommandResult> updateExpenseCommand,
        ICommandService<DeleteEntityCommand<Expense>, EmptyCommandResult> deleteExpenseCommand,
        IAsyncEventHandler<ExpenseUpdatedEvent, EmptyEventResult> expenseUpdatedEvent)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _createExpenseCommand = createExpenseCommand ?? throw new ArgumentNullException(nameof(createExpenseCommand));
        _updateExpenseCommand = updateExpenseCommand ?? throw new ArgumentNullException(nameof(updateExpenseCommand));
        _deleteExpenseCommand = deleteExpenseCommand ?? throw new ArgumentNullException(nameof(deleteExpenseCommand));
        _expenseUpdatedEvent = expenseUpdatedEvent ?? throw new ArgumentNullException(nameof(expenseUpdatedEvent));
    }

    public async Task<Guid?> CreateAsync(NewExpenseDto newExpense)
    {
        Expense expense = _mapper.Map<NewExpenseDto, Expense>(newExpense)!;

        CreateExpenseCommandResult result = await _createExpenseCommand.ExecuteAsync(new CreateExpenseCommand(expense));

        return result.Data;
    }

    public Task DeleteAsync(Guid id)
    {
        return _deleteExpenseCommand.ExecuteAsync(new DeleteEntityCommand<Expense>(id));
    }

    public async Task<Guid?> UpdateAsync(Guid id, NewExpenseDto newExpense)
    {
        Expense expense = _mapper.Map<NewExpenseDto, Expense>(newExpense)!;

        UpdateExpenseCommandResult result = await _updateExpenseCommand.ExecuteAsync(new UpdateExpenseCommand(id, expense));

        if (result.Data.HasValue)
        {
            await _expenseUpdatedEvent.Handle(new ExpenseUpdatedEvent(result.Data.Value, result.OldImageLink, result.OldPdfLink));
        }

        return result.Data;
    }
}
