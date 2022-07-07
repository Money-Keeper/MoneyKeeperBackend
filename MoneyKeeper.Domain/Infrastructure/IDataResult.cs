namespace MoneyKeeper.Domain.Infrastructure;

public interface IDataResult<TResult> where TResult : notnull
{
    TResult Data { get; }
}
