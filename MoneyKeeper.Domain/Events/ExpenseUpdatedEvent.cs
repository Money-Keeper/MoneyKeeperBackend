using MoneyKeeper.Domain.Infrastructure.Events;

namespace MoneyKeeper.Domain.Events;

public sealed class ExpenseUpdatedEvent : IAsyncEvent<EmptyEventResult>
{
    public ExpenseUpdatedEvent(Guid expenseId, string? oldImageLink, string? oldPdfLink)
    {
        ExpenseId = expenseId;
        OldImageLink = oldImageLink;
        OldPdfLink = oldPdfLink;
    }

    public Guid ExpenseId { get; }
    public string? OldImageLink { get; }
    public string? OldPdfLink { get; }

}
