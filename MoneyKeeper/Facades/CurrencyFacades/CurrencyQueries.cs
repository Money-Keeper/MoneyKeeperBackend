using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries;
using MoneyKeeper.Domain.Queries.CurrencyQueries;
using MoneyKeeper.Facades.CurrencyFacades.Abstractions;

namespace MoneyKeeper.Facades.CurrencyFacades;

internal sealed class CurrencyQueries : ICurrencyQueries
{
    private readonly IQueryService<EntityExistsQuery<Currency>, bool> _currencyExistsService;
    private readonly IQueryService<GetCurrencyByIdQuery, Currency?> _getCurrencyByIdService;
    private readonly IQueryService<GetCurrenciesQuery, IEnumerable<Currency>> _getCurrenciesService;

    public CurrencyQueries(
        IQueryService<EntityExistsQuery<Currency>, bool> currencyExistsService,
        IQueryService<GetCurrencyByIdQuery, Currency?> getCurrencyByIdService,
        IQueryService<GetCurrenciesQuery, IEnumerable<Currency>> getCurrenciesService)
    {
        _currencyExistsService = currencyExistsService ?? throw new ArgumentNullException(nameof(currencyExistsService));
        _getCurrencyByIdService = getCurrencyByIdService ?? throw new ArgumentNullException(nameof(getCurrencyByIdService));
        _getCurrenciesService = getCurrenciesService ?? throw new ArgumentNullException(nameof(getCurrenciesService));
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _currencyExistsService.ExecuteAsync(new EntityExistsQuery<Currency>(id));
    }

    public Task<Currency?> GetAsync(Guid id)
    {
        return _getCurrencyByIdService.ExecuteAsync(new GetCurrencyByIdQuery(id));
    }

    public Task<IEnumerable<Currency>> GetAsync()
    {
        return _getCurrenciesService.ExecuteAsync(new GetCurrenciesQuery());
    }
}
