using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Commands;
using MoneyKeeper.Domain.Commands.CategoryCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.CategoryFacades;

public sealed class CategoryCommandsService : ICategoryCommandsService
{
    private readonly IMapper _mapper;
    private readonly ICommandService<CreateCategoryCommand, CreateCategoryCommandResult> _createCategoryCommand;
    private readonly ICommandService<UpdateCategoryCommand, UpdateCategoryCommandResult> _updateCategoryCommand;
    private readonly ICommandService<DeleteEntityCommand<Category>, EmptyCommandResult> _deleteCategoryCommand;

    public CategoryCommandsService(
        IMapper mapper,
        ICommandService<CreateCategoryCommand, CreateCategoryCommandResult> createCategoryCommand,
        ICommandService<UpdateCategoryCommand, UpdateCategoryCommandResult> updateCategoryCommand,
        ICommandService<DeleteEntityCommand<Category>, EmptyCommandResult> deleteCategoryCommand)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _createCategoryCommand = createCategoryCommand ?? throw new ArgumentNullException(nameof(createCategoryCommand));
        _updateCategoryCommand = updateCategoryCommand ?? throw new ArgumentNullException(nameof(updateCategoryCommand));
        _deleteCategoryCommand = deleteCategoryCommand ?? throw new ArgumentNullException(nameof(deleteCategoryCommand));
    }

    public async Task<CategoryDto?> CreateAsync(NewCategoryDto newCategory)
    {
        Category category = _mapper.Map<NewCategoryDto, Category>(newCategory)!;

        CreateCategoryCommandResult result = await _createCategoryCommand.ExecuteAsync(new CreateCategoryCommand(category));

        CategoryDto? resultDto = _mapper.Map<Category, CategoryDto>(result.Data);

        return resultDto;
    }

    public Task DeleteAsync(Guid id)
    {
        return _deleteCategoryCommand.ExecuteAsync(new DeleteEntityCommand<Category>(id));
    }

    public async Task<CategoryDto?> UpdateAsync(Guid id, NewCategoryDto newCategory)
    {
        Category category = _mapper.Map<NewCategoryDto, Category>(newCategory)!;

        UpdateCategoryCommandResult result = await _updateCategoryCommand.ExecuteAsync(new UpdateCategoryCommand(id, category));

        CategoryDto? resultDto = _mapper.Map<Category, CategoryDto>(result.Data);

        return resultDto;
    }
}
