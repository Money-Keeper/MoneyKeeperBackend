namespace MoneyKeeper.Domain.Infrastructure;

public interface IDataResult<TResult>
{
    TResult Data { get; }
}
