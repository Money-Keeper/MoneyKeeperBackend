using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Queries.CategoryQueries;

public sealed class GetCategoryByIdQuery : IQuery<Category?>
{
    public GetCategoryByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
