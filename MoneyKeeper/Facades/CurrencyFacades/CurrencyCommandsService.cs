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
    private readonly ICommandService<CreateCurrencyCommand, CreateCurrencyCommandResult> _createCurrencyCommand;
    private readonly ICommandService<UpdateCurrencyCommand, UpdateCurrencyCommandResult> _updateCurrencyCommand;
    private readonly ICommandService<DeleteEntityCommand<Currency>, EmptyCommandResult> _deleteCurrencyCommand;

    public CurrencyCommandsService(
        IMapper mapper,
        ICommandService<CreateCurrencyCommand, CreateCurrencyCommandResult> createCurrencyCommand,
        ICommandService<UpdateCurrencyCommand, UpdateCurrencyCommandResult> updateCurrencyCommand,
        ICommandService<DeleteEntityCommand<Currency>, EmptyCommandResult> deleteCurrencyCommand)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _createCurrencyCommand = createCurrencyCommand ?? throw new ArgumentNullException(nameof(createCurrencyCommand));
        _updateCurrencyCommand = updateCurrencyCommand ?? throw new ArgumentNullException(nameof(updateCurrencyCommand));
        _deleteCurrencyCommand = deleteCurrencyCommand ?? throw new ArgumentNullException(nameof(deleteCurrencyCommand));
    }

    public async Task<CurrencyDto?> CreateAsync(NewCurrencyDto newCurrency)
    {
        Currency currency = _mapper.Map<NewCurrencyDto, Currency>(newCurrency)!;

        CreateCurrencyCommandResult result = await _createCurrencyCommand.ExecuteAsync(new CreateCurrencyCommand(currency));

        CurrencyDto? resultDto = _mapper.Map<Currency, CurrencyDto>(result.Data);

        return resultDto;
    }

    public Task DeleteAsync(Guid id)
    {
        return _deleteCurrencyCommand.ExecuteAsync(new DeleteEntityCommand<Currency>(id));
    }

    public async Task<CurrencyDto?> UpdateAsync(Guid id, NewCurrencyDto newCurrency)
    {
        Currency currency = _mapper.Map<NewCurrencyDto, Currency>(newCurrency)!;

        UpdateCurrencyCommandResult result = await _updateCurrencyCommand.ExecuteAsync(new UpdateCurrencyCommand(id, currency));

        CurrencyDto? resultDto = _mapper.Map<Currency, CurrencyDto>(result.Data);

        return resultDto;
    }
}
