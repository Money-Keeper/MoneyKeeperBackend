namespace MoneyKeeper.Domain.Infrastructure.Commands;

public interface ICommand<TResult> where TResult : ICommandResult
{
}
