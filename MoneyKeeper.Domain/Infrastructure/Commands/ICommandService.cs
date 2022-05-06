namespace MoneyKeeper.Domain.Infrastructure.Commands;

public interface ICommandService<TParameter, TResult>
    where TParameter : ICommand<TResult>
    where TResult : ICommandResult
{
    Task<TResult> ExecuteAsync(TParameter parameter);
}
