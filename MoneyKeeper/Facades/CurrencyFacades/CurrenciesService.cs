using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.CurrencyFacades.Abstractions;

namespace MoneyKeeper.Facades.CurrencyFacades;

internal sealed class CurrenciesService : ICurrenciesService
{
    private readonly IMapper _mapper;
    private readonly ICurrencyCommands _currencyCommands;
    private readonly ICurrencyQueries _currencyQueries;

    public CurrenciesService(IMapper mapper, ICurrencyCommands currencyCommands, ICurrencyQueries currencyQueries)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _currencyCommands = currencyCommands ?? throw new ArgumentNullException(nameof(currencyCommands));
        _currencyQueries = currencyQueries ?? throw new ArgumentNullException(nameof(currencyQueries));
    }

    public async Task<CurrencyDto> CreateAsync(NewCurrencyDto dto)
    {
        Currency result = await _currencyCommands.CreateAsync(_mapper.Map<NewCurrencyDto, Currency>(dto)!);
        return _mapper.Map<Currency, CurrencyDto>(result)!;
    }

    public Task DeleteAsync(Guid id) => _currencyCommands.DeleteAsync(id);

    public Task<bool> ExistsAsync(Guid id) => _currencyQueries.ExistsAsync(id);

    public async Task<CurrencyDto?> GetAsync(Guid id)
    {
        Currency? result = await _currencyQueries.GetAsync(id);
        return _mapper.Map<Currency, CurrencyDto>(result);
    }

    public async Task<DataResult<CurrencyDto>> GetAsync()
    {
        IEnumerable<Currency> result = await _currencyQueries.GetAsync();
        return new DataResult<CurrencyDto>(_mapper.Map<Currency, CurrencyDto>(result)!);
    }

    public async Task<CurrencyDto> UpdateAsync(Guid id, NewCurrencyDto dto)
    {
        var result = await _currencyCommands.UpdateAsync(id, _mapper.Map<NewCurrencyDto, Currency>(dto)!);
        return _mapper.Map<Currency, CurrencyDto>(result)!;
    }
}
