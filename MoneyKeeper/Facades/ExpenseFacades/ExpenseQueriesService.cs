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
    private readonly IQueryService<EntityExistsQuery<Expense>, bool> _expenseExistsQuery;
    private readonly IQueryService<GetExpenseByIdQuery, Expense?> _getExpenseByIdQuery;
    private readonly IQueryService<GetExpensesByConditionQuery, IEnumerable<Expense>> _getExpensesByConditionQuery;

    public ExpenseQueriesService(
        IMapper mapper,
        IQueryService<EntityExistsQuery<Expense>, bool> expenseExistsQuery,
        IQueryService<GetExpenseByIdQuery, Expense?> getExpenseByIdQuery,
        IQueryService<GetExpensesByConditionQuery, IEnumerable<Expense>> getExpensesByConditionQuery)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _expenseExistsQuery = expenseExistsQuery ?? throw new ArgumentNullException(nameof(expenseExistsQuery));
        _getExpenseByIdQuery = getExpenseByIdQuery ?? throw new ArgumentNullException(nameof(getExpenseByIdQuery));
        _getExpensesByConditionQuery = getExpensesByConditionQuery ?? throw new ArgumentNullException(nameof(getExpensesByConditionQuery));
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _expenseExistsQuery.ExecuteAsync(new EntityExistsQuery<Expense>(id));
    }

    public async Task<ExpenseDto?> GetAsync(Guid id)
    {
        Expense? result = await _getExpenseByIdQuery.ExecuteAsync(new GetExpenseByIdQuery(id));

        return _mapper.Map<Expense, ExpenseDto>(result);
    }

    public async Task<DataResult<ExpenseDto>> GetAsync()
    {
        IEnumerable<Expense> result = await _getExpensesByConditionQuery.ExecuteAsync(new GetExpensesByConditionQuery());
        IEnumerable<ExpenseDto> resultDto = _mapper.Map<Expense, ExpenseDto>(result);

        return new DataResult<ExpenseDto>(resultDto);
    }
}
