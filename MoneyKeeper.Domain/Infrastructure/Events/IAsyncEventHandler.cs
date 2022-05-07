namespace MoneyKeeper.Domain.Infrastructure.Events;

public interface IAsyncEventHandler<TEvent> where TEvent : IAsyncEvent
{
    Task HandleAsync(TEvent e);
}
