using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Domain.Providers;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime NowUtc => DateTime.Now.ToUniversalTime();
}
