using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Commands.ExpenseCommands;
using MoneyKeeper.Domain.Events;
using MoneyKeeper.Domain.Infrastructure.Events;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.ExpenseFacades.Abstractions;

namespace MoneyKeeper.Facades.ExpenseFacades;

internal sealed class ExpensesService : IExpensesService
{
    private readonly IMapper _mapper;
    private readonly IExpenseCommands _expenseCommands;
    private readonly IExpenseQueries _expenseQueries;
    private readonly IAsyncEventHandler<ExpenseUpdatedEvent> _expenseUpdatedHandler;

    public ExpensesService(
        IMapper mapper,
        IExpenseCommands expenseCommands,
        IExpenseQueries expenseQueries,
        IAsyncEventHandler<ExpenseUpdatedEvent> expenseUpdatedHandler)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _expenseCommands = expenseCommands ?? throw new ArgumentNullException(nameof(expenseCommands));
        _expenseQueries = expenseQueries ?? throw new ArgumentNullException(nameof(expenseQueries));
        _expenseUpdatedHandler = expenseUpdatedHandler ?? throw new ArgumentNullException(nameof(expenseUpdatedHandler));
    }

    public async Task<ExpenseDto> CreateAsync(NewExpenseDto dto)
    {
        Guid result = await _expenseCommands.CreateAsync(_mapper.Map<NewExpenseDto, Expense>(dto)!);
        return _mapper.Map<Expense, ExpenseDto>(await _expenseQueries.GetAsync(result))!;
    }

    public Task DeleteAsync(Guid id) => _expenseCommands.DeleteAsync(id);

    public Task<bool> ExistsAsync(Guid id) => _expenseQueries.ExistsAsync(id);

    public async Task<ExpenseDto?> GetAsync(Guid id)
    {
        Expense? result = await _expenseQueries.GetAsync(id);
        return _mapper.Map<Expense, ExpenseDto>(result);
    }

    public async Task<DataResult<ExpenseDto>> GetAsync(ExpenseQueryCondition condition)
    {
        IEnumerable<Expense> result = await _expenseQueries.GetAsync(condition);
        return new DataResult<ExpenseDto>(_mapper.Map<Expense, ExpenseDto>(result));
    }

    public async Task<ExpenseDto> UpdateAsync(Guid id, NewExpenseDto dto)
    {
        UpdateExpenseCommandResult result = await _expenseCommands.UpdateAsync(id, _mapper.Map<NewExpenseDto, Expense>(dto)!);

        await _expenseUpdatedHandler.HandleAsync(new ExpenseUpdatedEvent(result.Data, result.OldImageLink, result.OldPdfLink));

        return _mapper.Map<Expense, ExpenseDto>(await _expenseQueries.GetAsync(id))!;
    }
}
