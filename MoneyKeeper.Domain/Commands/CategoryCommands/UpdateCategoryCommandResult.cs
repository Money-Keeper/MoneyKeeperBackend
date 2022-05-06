using MoneyKeeper.Domain.Infrastructure;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Domain.Commands.CategoryCommands;

public sealed class UpdateCategoryCommandResult : ICommandResult, IDataResult<Category?>
{
    public UpdateCategoryCommandResult(Category? data)
    {
        Data = data;
    }

    public Category? Data { get; }
}
