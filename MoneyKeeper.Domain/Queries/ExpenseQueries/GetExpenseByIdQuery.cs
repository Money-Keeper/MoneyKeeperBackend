using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Queries.ExpenseQueries;

public sealed class GetExpenseByIdQuery : IQuery<Expense?>
{
    public GetExpenseByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
