using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands;

public sealed class DeleteEntityCommand<TEntity> : ICommand<EmptyCommandResult> where TEntity : BaseModel
{
    public DeleteEntityCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
