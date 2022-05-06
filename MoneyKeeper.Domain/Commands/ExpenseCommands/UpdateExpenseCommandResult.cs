using MoneyKeeper.Domain.Infrastructure;
using MoneyKeeper.Domain.Infrastructure.Commands;

namespace MoneyKeeper.Domain.Commands.ExpenseCommands;

public sealed class UpdateExpenseCommandResult : ICommandResult, IDataResult<Guid?>
{
    public UpdateExpenseCommandResult(Guid? data, string? oldImageLink, string? oldPdfLink)
    {
        Data = data;
        OldImageLink = oldImageLink;
        OldPdfLink = oldPdfLink;
    }

    public Guid? Data { get; }
    public string? OldImageLink { get; }
    public string? OldPdfLink { get; }
}
