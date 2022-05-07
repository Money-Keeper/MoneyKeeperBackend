using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Queries.ExpenseQueries;

public sealed class GetExpensesByConditionQuery : IQuery<IEnumerable<Expense>>
{
    public GetExpensesByConditionQuery(Guid categoryId, long? dateFrom, long? dateTo)
    {
        CategoryId = categoryId;
        DateFrom = dateFrom;
        DateTo = dateTo;
    }

    public Guid CategoryId { get; }
    public long? DateFrom { get; }
    public long? DateTo { get; }
}
