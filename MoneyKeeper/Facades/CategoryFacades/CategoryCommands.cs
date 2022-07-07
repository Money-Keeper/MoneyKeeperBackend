using MoneyKeeper.Domain.Commands;
using MoneyKeeper.Domain.Commands.CategoryCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Facades.CategoryFacades.Abstractions;

namespace MoneyKeeper.Facades.CategoryFacades;

internal sealed class CategoryCommands : ICategoryCommands
{
    private readonly ICommandService<CreateCategoryCommand, CreateCategoryCommandResult> _createCategoryService;
    private readonly ICommandService<UpdateCategoryCommand, UpdateCategoryCommandResult> _updateCategoryService;
    private readonly ICommandService<DeleteEntityCommand<Category>, EmptyCommandResult> _deleteCategoryService;

    public CategoryCommands(
        ICommandService<CreateCategoryCommand, CreateCategoryCommandResult> createCategoryService,
        ICommandService<UpdateCategoryCommand, UpdateCategoryCommandResult> updateCategoryService,
        ICommandService<DeleteEntityCommand<Category>, EmptyCommandResult> deleteCategoryService)
    {
        _createCategoryService = createCategoryService ?? throw new ArgumentNullException(nameof(createCategoryService));
        _updateCategoryService = updateCategoryService ?? throw new ArgumentNullException(nameof(updateCategoryService));
        _deleteCategoryService = deleteCategoryService ?? throw new ArgumentNullException(nameof(deleteCategoryService));
    }

    public async Task<Category> CreateAsync(Category category)
    {
        return (await _createCategoryService.ExecuteAsync(new CreateCategoryCommand(category))).Data;
    }

    public Task DeleteAsync(Guid id)
    {
        return _deleteCategoryService.ExecuteAsync(new DeleteEntityCommand<Category>(id));
    }

    public async Task<Category> UpdateAsync(Guid id, Category category)
    {
        return (await _updateCategoryService.ExecuteAsync(new UpdateCategoryCommand(id, category))).Data;
    }
}
