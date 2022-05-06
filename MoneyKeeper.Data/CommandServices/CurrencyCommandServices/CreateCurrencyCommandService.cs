using MoneyKeeper.Domain.Commands.CurrencyCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Data.CommandServices.CurrencyCommandServices;

public sealed class CreateCurrencyCommandService : ICommandService<CreateCurrencyCommand, CreateCurrencyCommandResult>
{
    private readonly AppDbContext _dbContext;

    public CreateCurrencyCommandService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<CreateCurrencyCommandResult> ExecuteAsync(CreateCurrencyCommand parameter)
    {
        Currency result = _dbContext.Currencies.Add(parameter.NewCurrency).Entity;

        await _dbContext.SaveChangesAsync();

        return new CreateCurrencyCommandResult(result);
    }
}
