namespace MoneyKeeper.Domain.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime NowUtc => DateTime.Now.ToUniversalTime();
}
