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
    private readonly IQueryService<EntityExistsQuery<Currency>, bool> _currencyExistsService;
    private readonly IQueryService<GetCurrencyByIdQuery, Currency?> _getCurrencyByIdService;
    private readonly IQueryService<GetCurrenciesQuery, IEnumerable<Currency>> _getCurrenciesService;

    public CurrencyQueriesService(
        IMapper mapper,
        IQueryService<EntityExistsQuery<Currency>, bool> currencyExistsService,
        IQueryService<GetCurrencyByIdQuery, Currency?> getCurrencyByIdService,
        IQueryService<GetCurrenciesQuery, IEnumerable<Currency>> getCurrenciesService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _currencyExistsService = currencyExistsService ?? throw new ArgumentNullException(nameof(currencyExistsService));
        _getCurrencyByIdService = getCurrencyByIdService ?? throw new ArgumentNullException(nameof(getCurrencyByIdService));
        _getCurrenciesService = getCurrenciesService ?? throw new ArgumentNullException(nameof(getCurrenciesService));
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _currencyExistsService.ExecuteAsync(new EntityExistsQuery<Currency>(id));
    }

    public async Task<CurrencyDto?> GetAsync(Guid id)
    {
        Currency? result = await _getCurrencyByIdService.ExecuteAsync(new GetCurrencyByIdQuery(id));

        return _mapper.Map<Currency, CurrencyDto>(result);
    }

    public async Task<DataResult<CurrencyDto>> GetAsync()
    {
        IEnumerable<Currency> result = await _getCurrenciesService.ExecuteAsync(new GetCurrenciesQuery());
        IEnumerable<CurrencyDto> resultDto = _mapper.Map<Currency, CurrencyDto>(result);

        return new DataResult<CurrencyDto>(resultDto);
    }
}
