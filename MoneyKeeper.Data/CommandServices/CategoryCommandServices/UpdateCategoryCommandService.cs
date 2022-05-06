using MoneyKeeper.Domain.Commands.CategoryCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Data.CommandServices.CategoryCommandServices;

public sealed class UpdateCategoryCommandService : ICommandService<UpdateCategoryCommand, UpdateCategoryCommandResult>
{
    private readonly AppDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateCategoryCommandService(AppDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public async Task<UpdateCategoryCommandResult> ExecuteAsync(UpdateCategoryCommand parameter)
    {
        Category category = parameter.NewCategory;

        if (category.ParentCategoryId.HasValue)
        {
            bool isCategoryExists = await _dbContext.EntityExistsAsync<Category>(category.ParentCategoryId.Value);

            if (!isCategoryExists)
                return new UpdateCategoryCommandResult(null);
        }

        category.Id = parameter.Id;
        category.ModifiedAt = _dateTimeProvider.NowUtc;

        _dbContext.Categories.Update(category);

        await _dbContext.SaveChangesAsync();

        return new UpdateCategoryCommandResult(category);
    }
}
