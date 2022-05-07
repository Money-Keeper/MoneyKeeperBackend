using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Commands;
using MoneyKeeper.Domain.Commands.CurrencyCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.CurrencyFacades;

public sealed class CurrencyCommandsService : ICurrencyCommandsService
{
    private readonly IMapper _mapper;
    private readonly ICommandService<CreateCurrencyCommand, CreateCurrencyCommandResult> _createCurrencyService;
    private readonly ICommandService<UpdateCurrencyCommand, UpdateCurrencyCommandResult> _updateCurrencyService;
    private readonly ICommandService<DeleteEntityCommand<Currency>, EmptyCommandResult> _deleteCurrencyService;

    public CurrencyCommandsService(
        IMapper mapper,
        ICommandService<CreateCurrencyCommand, CreateCurrencyCommandResult> createCurrencyService,
        ICommandService<UpdateCurrencyCommand, UpdateCurrencyCommandResult> updateCurrencyService,
        ICommandService<DeleteEntityCommand<Currency>, EmptyCommandResult> deleteCurrencyService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _createCurrencyService = createCurrencyService ?? throw new ArgumentNullException(nameof(createCurrencyService));
        _updateCurrencyService = updateCurrencyService ?? throw new ArgumentNullException(nameof(updateCurrencyService));
        _deleteCurrencyService = deleteCurrencyService ?? throw new ArgumentNullException(nameof(deleteCurrencyService));
    }

    public async Task<CurrencyDto?> CreateAsync(NewCurrencyDto newCurrency)
    {
        Currency currency = _mapper.Map<NewCurrencyDto, Currency>(newCurrency)!;

        CreateCurrencyCommandResult result = await _createCurrencyService.ExecuteAsync(new CreateCurrencyCommand(currency));

        CurrencyDto? resultDto = _mapper.Map<Currency, CurrencyDto>(result.Data);

        return resultDto;
    }

    public Task DeleteAsync(Guid id)
    {
        return _deleteCurrencyService.ExecuteAsync(new DeleteEntityCommand<Currency>(id));
    }

    public async Task<CurrencyDto?> UpdateAsync(Guid id, NewCurrencyDto newCurrency)
    {
        Currency currency = _mapper.Map<NewCurrencyDto, Currency>(newCurrency)!;

        UpdateCurrencyCommandResult result = await _updateCurrencyService.ExecuteAsync(new UpdateCurrencyCommand(id, currency));

        CurrencyDto? resultDto = _mapper.Map<Currency, CurrencyDto>(result.Data);

        return resultDto;
    }
}
