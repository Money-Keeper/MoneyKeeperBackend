using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries;
using MoneyKeeper.Domain.Queries.ExpenseQueries;
using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.ExpenseFacades;

public sealed class ExpenseQueriesService : IExpenseQueriesService
{
    private readonly IMapper _mapper;
    private readonly IQueryService<EntityExistsQuery<Expense>, bool> _expenseExistsService;
    private readonly IQueryService<GetExpenseByIdQuery, Expense?> _getExpenseByIdService;
    private readonly IQueryService<GetExpensesByConditionQuery, IEnumerable<Expense>> _getExpensesByConditionService;

    public ExpenseQueriesService(
        IMapper mapper,
        IQueryService<EntityExistsQuery<Expense>, bool> expenseExistsService,
        IQueryService<GetExpenseByIdQuery, Expense?> getExpenseByIdService,
        IQueryService<GetExpensesByConditionQuery, IEnumerable<Expense>> getExpensesByConditionService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _expenseExistsService = expenseExistsService ?? throw new ArgumentNullException(nameof(expenseExistsService));
        _getExpenseByIdService = getExpenseByIdService ?? throw new ArgumentNullException(nameof(getExpenseByIdService));
        _getExpensesByConditionService = getExpensesByConditionService ?? throw new ArgumentNullException(nameof(getExpensesByConditionService));
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _expenseExistsService.ExecuteAsync(new EntityExistsQuery<Expense>(id));
    }

    public async Task<ExpenseDto?> GetAsync(Guid id)
    {
        Expense? result = await _getExpenseByIdService.ExecuteAsync(new GetExpenseByIdQuery(id));

        return _mapper.Map<Expense, ExpenseDto>(result);
    }

    public async Task<DataResult<ExpenseDto>> GetAsync(ExpenseConditionDto condition)
    {
        var query = new GetExpensesByConditionQuery(condition.CategoryId!.Value, condition.DateFrom, condition.DateTo);

        IEnumerable<Expense> result = await _getExpensesByConditionService.ExecuteAsync(query);
        IEnumerable<ExpenseDto> resultDto = _mapper.Map<Expense, ExpenseDto>(result);

        return new DataResult<ExpenseDto>(resultDto);
    }
}
