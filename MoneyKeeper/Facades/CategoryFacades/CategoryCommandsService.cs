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
    private readonly ICommandService<CreateCategoryCommand, CreateCategoryCommandResult> _createCategoryService;
    private readonly ICommandService<UpdateCategoryCommand, UpdateCategoryCommandResult> _updateCategoryService;
    private readonly ICommandService<DeleteEntityCommand<Category>, EmptyCommandResult> _deleteCategoryService;

    public CategoryCommandsService(
        IMapper mapper,
        ICommandService<CreateCategoryCommand, CreateCategoryCommandResult> createCategoryService,
        ICommandService<UpdateCategoryCommand, UpdateCategoryCommandResult> updateCategoryService,
        ICommandService<DeleteEntityCommand<Category>, EmptyCommandResult> deleteCategoryService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _createCategoryService = createCategoryService ?? throw new ArgumentNullException(nameof(createCategoryService));
        _updateCategoryService = updateCategoryService ?? throw new ArgumentNullException(nameof(updateCategoryService));
        _deleteCategoryService = deleteCategoryService ?? throw new ArgumentNullException(nameof(deleteCategoryService));
    }

    public async Task<CategoryDto?> CreateAsync(NewCategoryDto newCategory)
    {
        Category category = _mapper.Map<NewCategoryDto, Category>(newCategory)!;

        CreateCategoryCommandResult result = await _createCategoryService.ExecuteAsync(new CreateCategoryCommand(category));

        return _mapper.Map<Category, CategoryDto>(result.Data);
    }

    public Task DeleteAsync(Guid id)
    {
        return _deleteCategoryService.ExecuteAsync(new DeleteEntityCommand<Category>(id));
    }

    public async Task<CategoryDto?> UpdateAsync(Guid id, NewCategoryDto newCategory)
    {
        Category category = _mapper.Map<NewCategoryDto, Category>(newCategory)!;

        UpdateCategoryCommandResult result = await _updateCategoryService.ExecuteAsync(new UpdateCategoryCommand(id, category));

        return _mapper.Map<Category, CategoryDto>(result.Data);
    }
}
