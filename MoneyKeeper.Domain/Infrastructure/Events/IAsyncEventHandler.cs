namespace MoneyKeeper.Domain.Infrastructure.Events;

public interface IAsyncEventHandler<TEvent, TResult>
    where TEvent : IAsyncEvent<TResult>
    where TResult : IEventResult
{
    Task<TResult> Handle(TEvent e);
}
