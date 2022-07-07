using MoneyKeeper.Domain.Infrastructure;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.CategoryCommands;

public sealed class CreateCategoryCommandResult : ICommandResult, IDataResult<Category>
{
    public CreateCategoryCommandResult(Category data)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public Category Data { get; }
}
