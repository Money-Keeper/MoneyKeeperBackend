using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Queries.CurrencyQueries;

public sealed class GetCurrenciesQuery : IQuery<IEnumerable<Currency>>
{
}
