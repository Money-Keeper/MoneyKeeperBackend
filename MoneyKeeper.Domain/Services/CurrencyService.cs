using MoneyKeeper.Domain.AutoMapper;
using MoneyKeeper.Domain.Data.Models;
using MoneyKeeper.Domain.Data.Repositories;
using MoneyKeeper.Domain.Dtos;

namespace MoneyKeeper.Domain.Services;

public sealed class CurrencyService : ICurrencyService
{
    private readonly IMapper _mapper;
    private readonly ICurrencyRepository _currencyRepository;

    public CurrencyService(IMapper mapper, ICurrencyRepository currencyRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _currencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
    }

    public async Task<CurrencyDto?> GetAsync(Guid id)
    {
        Currency? result = await _currencyRepository.GetAsync(id);

        return _mapper.Map<Currency, CurrencyDto>(result);
    }

    public async Task<DataResult<CurrencyDto>> GetAsync()
    {
        IEnumerable<Currency> result = await _currencyRepository.GetAsync();
        IEnumerable<CurrencyDto> resultDto = _mapper.Map<Currency, CurrencyDto>(result);

        return new DataResult<CurrencyDto>(resultDto);
    }

    public Task<bool> CreateAsync(NewCurrencyDto currencyDto)
    {
        Currency currency = _mapper.Map<NewCurrencyDto, Currency>(currencyDto)!;

        return _currencyRepository.CreateAsync(currency);
    }

    public Task<bool> UpdateAsync(Guid id, NewCurrencyDto currencyDto)
    {
        Currency currency = _mapper.Map<NewCurrencyDto, Currency>(currencyDto)!;

        return _currencyRepository.UpdateAsync(id, currency);
    }

    public Task DeleteAsync(Guid id)
    {
        return _currencyRepository.DeleteAsync(id);
    }
}
