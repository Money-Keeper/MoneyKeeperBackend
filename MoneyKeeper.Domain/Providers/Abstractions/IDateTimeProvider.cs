namespace MoneyKeeper.Domain.Providers.Abstractions;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime NowUtc { get; }
}
