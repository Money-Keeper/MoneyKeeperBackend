using MoneyKeeper.Domain.Commands.CurrencyCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Data.CommandServices.CurrencyCommandServices;

public sealed class UpdateCurrencyCommandService : ICommandService<UpdateCurrencyCommand, UpdateCurrencyCommandResult>
{
    private readonly AppDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateCurrencyCommandService(AppDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public async Task<UpdateCurrencyCommandResult> ExecuteAsync(UpdateCurrencyCommand parameter)
    {
        Currency currency = parameter.NewCurrency;

        currency.Id = parameter.Id;
        currency.ModifiedAt = _dateTimeProvider.NowUtc;

        _dbContext.Currencies.Update(currency);

        await _dbContext.SaveChangesAsync();

        return new UpdateCurrencyCommandResult(currency);
    }
}
