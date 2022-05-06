using MoneyKeeper.Domain.Infrastructure;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.CurrencyCommands;

public sealed class CreateCurrencyCommandResult : ICommandResult, IDataResult<Currency?>
{
    public CreateCurrencyCommandResult(Currency? data)
    {
        Data = data;
    }

    public Currency? Data { get; }
}
