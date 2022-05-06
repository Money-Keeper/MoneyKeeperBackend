namespace MoneyKeeper.Domain.Infrastructure.Queries;

public interface IQueryService<TParameter, TResult> where TParameter : IQuery<TResult>
{
    Task<TResult> ExecuteAsync(TParameter parameter);
}
