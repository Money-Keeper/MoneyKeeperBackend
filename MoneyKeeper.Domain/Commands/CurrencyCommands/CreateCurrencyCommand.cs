using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.CurrencyCommands;

public sealed class CreateCurrencyCommand : ICommand<CreateCurrencyCommandResult>
{
    public CreateCurrencyCommand(Currency newCurrency)
    {
        NewCurrency = newCurrency ?? throw new ArgumentNullException(nameof(newCurrency));
    }

    public Currency NewCurrency { get; }
}
