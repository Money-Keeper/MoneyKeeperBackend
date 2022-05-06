using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.CategoryCommands;

public sealed class UpdateCategoryCommand : ICommand<UpdateCategoryCommandResult>
{
    public UpdateCategoryCommand(Guid id, Category newCategory)
    {
        Id = id;
        NewCategory = newCategory ?? throw new ArgumentNullException(nameof(newCategory));
    }

    public Guid Id { get; }
    public Category NewCategory { get; }
}
