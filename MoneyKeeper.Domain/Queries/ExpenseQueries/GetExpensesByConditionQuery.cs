using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Queries.ExpenseQueries;

public sealed class GetExpensesByConditionQuery : IQuery<IEnumerable<Expense>>
{
    public GetExpensesByConditionQuery()
    {

    }
}
