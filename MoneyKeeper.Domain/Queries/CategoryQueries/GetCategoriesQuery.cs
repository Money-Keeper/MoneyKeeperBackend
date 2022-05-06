using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Queries.CategoryQueries;

public sealed class GetCategoriesQuery : IQuery<IEnumerable<Category>>
{
}
