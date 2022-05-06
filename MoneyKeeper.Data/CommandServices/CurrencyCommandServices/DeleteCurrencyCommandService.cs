using MoneyKeeper.Domain.Commands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Data.CommandServices.CurrencyCommandServices;

public sealed class DeleteCurrencyCommandService : ICommandService<DeleteEntityCommand<Currency>, EmptyCommandResult>
{
    private readonly AppDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public DeleteCurrencyCommandService(AppDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public async Task<EmptyCommandResult> ExecuteAsync(DeleteEntityCommand<Currency> parameter)
    {
        Currency currency = _dbContext.Currencies.Attach(new Currency { Id = parameter.Id }).Entity;

        currency.DeletedAt = _dateTimeProvider.NowUtc;

        await _dbContext.SaveChangesAsync();

        return new EmptyCommandResult();
    }
}
