using MoneyKeeper.Domain.Data.Abstractions;
using MoneyKeeper.Domain.Data.Abstractions.Repositories;
using MoneyKeeper.Domain.Data.Models;
using MoneyKeeper.Domain.Dtos;
using MoneyKeeper.Domain.Services.Abstractions;
using MoneyKeeper.Domain.Tools.Abstractions;

namespace MoneyKeeper.Domain.Services;

public sealed class CurrencyService : ICurrencyService
{
    private readonly IMapper _mapper;
    private readonly IEntityHelper _entityHelper;
    private readonly ICurrencyRepository _currencyRepository;

    public CurrencyService(IMapper mapper, IEntityHelper entityHelper, ICurrencyRepository currencyRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _entityHelper = entityHelper ?? throw new ArgumentNullException(nameof(entityHelper));
        _currencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _entityHelper.ExistsAsync<Currency>(id);
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
