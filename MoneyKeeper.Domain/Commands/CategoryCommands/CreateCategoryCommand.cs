using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.CategoryCommands;

public sealed class CreateCategoryCommand : ICommand<CreateCategoryCommandResult>
{
    public CreateCategoryCommand(Category newCategory)
    {
        NewCategory = newCategory ?? throw new ArgumentNullException(nameof(newCategory));
    }

    public Category NewCategory { get; }
}
