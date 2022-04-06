namespace MoneyKeeper.Domain.Providers;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime NowUtc { get; }
}
