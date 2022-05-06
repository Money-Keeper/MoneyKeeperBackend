using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries;
using MoneyKeeper.Domain.Queries.CurrencyQueries;
using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.CurrencyFacades;

public sealed class CurrencyQueriesService : ICurrencyQueriesService
{
    private readonly IMapper _mapper;
    private readonly IQueryService<EntityExistsQuery<Currency>, bool> _currencyExistsQuery;
    private readonly IQueryService<GetCurrencyByIdQuery, Currency?> _getCurrencyByIdQuery;
    private readonly IQueryService<GetCurrenciesQuery, IEnumerable<Currency>> _getCurrenciesQuery;

    public CurrencyQueriesService(
        IMapper mapper,
        IQueryService<EntityExistsQuery<Currency>, bool> currencyExistsQuery,
        IQueryService<GetCurrencyByIdQuery, Currency?> getCurrencyByIdQuery,
        IQueryService<GetCurrenciesQuery, IEnumerable<Currency>> getCurrenciesQuery)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _currencyExistsQuery = currencyExistsQuery ?? throw new ArgumentNullException(nameof(currencyExistsQuery));
        _getCurrencyByIdQuery = getCurrencyByIdQuery ?? throw new ArgumentNullException(nameof(getCurrencyByIdQuery));
        _getCurrenciesQuery = getCurrenciesQuery ?? throw new ArgumentNullException(nameof(getCurrenciesQuery));
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _currencyExistsQuery.ExecuteAsync(new EntityExistsQuery<Currency>(id));
    }

    public async Task<CurrencyDto?> GetAsync(Guid id)
    {
        Currency? result = await _getCurrencyByIdQuery.ExecuteAsync(new GetCurrencyByIdQuery(id));

        return _mapper.Map<Currency, CurrencyDto>(result);
    }

    public async Task<DataResult<CurrencyDto>> GetAsync()
    {
        IEnumerable<Currency> result = await _getCurrenciesQuery.ExecuteAsync(new GetCurrenciesQuery());
        IEnumerable<CurrencyDto> resultDto = _mapper.Map<Currency, CurrencyDto>(result);

        return new DataResult<CurrencyDto>(resultDto);
    }
}
