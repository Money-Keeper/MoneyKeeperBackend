using MoneyKeeper.Domain.Commands;
using MoneyKeeper.Domain.Commands.CurrencyCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Facades.CurrencyFacades.Abstractions;

namespace MoneyKeeper.Facades.CurrencyFacades;

internal sealed class CurrencyCommands : ICurrencyCommands
{
    private readonly ICommandService<CreateCurrencyCommand, CreateCurrencyCommandResult> _createCurrencyService;
    private readonly ICommandService<UpdateCurrencyCommand, UpdateCurrencyCommandResult> _updateCurrencyService;
    private readonly ICommandService<DeleteEntityCommand<Currency>, EmptyCommandResult> _deleteCurrencyService;

    public CurrencyCommands(
        ICommandService<CreateCurrencyCommand, CreateCurrencyCommandResult> createCurrencyService,
        ICommandService<UpdateCurrencyCommand, UpdateCurrencyCommandResult> updateCurrencyService,
        ICommandService<DeleteEntityCommand<Currency>, EmptyCommandResult> deleteCurrencyService)
    {
        _createCurrencyService = createCurrencyService ?? throw new ArgumentNullException(nameof(createCurrencyService));
        _updateCurrencyService = updateCurrencyService ?? throw new ArgumentNullException(nameof(updateCurrencyService));
        _deleteCurrencyService = deleteCurrencyService ?? throw new ArgumentNullException(nameof(deleteCurrencyService));
    }

    public async Task<Currency> CreateAsync(Currency currency)
    {
        return (await _createCurrencyService.ExecuteAsync(new CreateCurrencyCommand(currency))).Data;
    }

    public Task DeleteAsync(Guid id)
    {
        return _deleteCurrencyService.ExecuteAsync(new DeleteEntityCommand<Currency>(id));
    }

    public async Task<Currency> UpdateAsync(Guid id, Currency currency)
    {
        return (await _updateCurrencyService.ExecuteAsync(new UpdateCurrencyCommand(id, currency))).Data;
    }
}
