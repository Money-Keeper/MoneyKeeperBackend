using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Queries.CurrencyQueries;

public sealed class GetCurrencyByIdQuery : IQuery<Currency?>
{
    public GetCurrencyByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
