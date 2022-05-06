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
        Category category = parameter.NewCategory;

        if (category.ParentCategoryId.HasValue)
        {
            bool isCategoryExists = await _dbContext.EntityExistsAsync<Category>(category.ParentCategoryId.Value);

            if (!isCategoryExists)
                return new CreateCategoryCommandResult(null);
        }

        Category result = _dbContext.Categories.Add(category).Entity;

        await _dbContext.SaveChangesAsync();

        return new CreateCategoryCommandResult(result);
    }
}
