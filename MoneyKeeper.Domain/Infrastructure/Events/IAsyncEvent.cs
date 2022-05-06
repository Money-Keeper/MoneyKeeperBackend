namespace MoneyKeeper.Domain.Infrastructure.Events;

public interface IAsyncEvent<TResult> where TResult : IEventResult
{
}
