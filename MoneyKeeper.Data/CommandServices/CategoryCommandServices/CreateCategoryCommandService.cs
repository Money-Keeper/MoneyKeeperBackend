using MoneyKeeper.Domain.Commands.CategoryCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Data.CommandServices.CategoryCommandServices;

public sealed class CreateCategoryCommandService : ICommandService<CreateCategoryCommand, CreateCategoryCommandResult>
{
    private readonly AppDbContext _dbContext;

    public CreateCategoryCommandService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<CreateCategoryCommandResult> ExecuteAsync(CreateCategoryCommand parameter)
    {
        Category result = _dbContext.Categories.Add(parameter.NewCategory).Entity;

        await _dbContext.SaveChangesAsync();

        return new CreateCategoryCommandResult(result);
    }
}
