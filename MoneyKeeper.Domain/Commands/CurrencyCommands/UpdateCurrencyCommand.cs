using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.CurrencyCommands;

public sealed class UpdateCurrencyCommand : ICommand<UpdateCurrencyCommandResult>
{
    public UpdateCurrencyCommand(Guid id, Currency newCurrency)
    {
        Id = id;
        NewCurrency = newCurrency ?? throw new ArgumentNullException(nameof(newCurrency));
    }

    public Guid Id { get; }
    public Currency NewCurrency { get; }
}
