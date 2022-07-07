using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries;
using MoneyKeeper.Domain.Queries.ExpenseQueries;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.ExpenseFacades.Abstractions;

namespace MoneyKeeper.Facades.ExpenseFacades;

internal sealed class ExpenseQueries : IExpenseQueries
{
    private readonly IQueryService<EntityExistsQuery<Expense>, bool> _expenseExistsService;
    private readonly IQueryService<GetExpenseByIdQuery, Expense?> _getExpenseByIdService;
    private readonly IQueryService<GetExpensesByConditionQuery, IEnumerable<Expense>> _getExpensesByConditionService;

    public ExpenseQueries(
        IQueryService<EntityExistsQuery<Expense>, bool> expenseExistsService,
        IQueryService<GetExpenseByIdQuery, Expense?> getExpenseByIdService,
        IQueryService<GetExpensesByConditionQuery, IEnumerable<Expense>> getExpensesByConditionService)
    {
        _expenseExistsService = expenseExistsService ?? throw new ArgumentNullException(nameof(expenseExistsService));
        _getExpenseByIdService = getExpenseByIdService ?? throw new ArgumentNullException(nameof(getExpenseByIdService));
        _getExpensesByConditionService = getExpensesByConditionService ?? throw new ArgumentNullException(nameof(getExpensesByConditionService));
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _expenseExistsService.ExecuteAsync(new EntityExistsQuery<Expense>(id));
    }

    public Task<Expense?> GetAsync(Guid id)
    {
        return _getExpenseByIdService.ExecuteAsync(new GetExpenseByIdQuery(id));
    }

    public Task<IEnumerable<Expense>> GetAsync(ExpenseConditionDto condition)
    {
        var query = new GetExpensesByConditionQuery(
            categoryId: condition.CategoryId!.Value,
            dateFrom: condition.DateFrom,
            dateTo: condition.DateTo);

        return _getExpensesByConditionService.ExecuteAsync(query);
    }
}
