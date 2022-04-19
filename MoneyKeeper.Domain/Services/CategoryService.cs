using MoneyKeeper.Domain.Data.Abstractions;
using MoneyKeeper.Domain.Data.Abstractions.Repositories;
using MoneyKeeper.Domain.Data.Models;
using MoneyKeeper.Domain.Dtos;
using MoneyKeeper.Domain.Services.Abstractions;
using MoneyKeeper.Domain.Tools.Abstractions;

namespace MoneyKeeper.Domain.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly IEntityHelper _entityHelper;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(IMapper mapper, IEntityHelper entityHelper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _entityHelper = entityHelper ?? throw new ArgumentNullException(nameof(entityHelper));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _entityHelper.ExistsAsync<Category>(id);
    }

    public async Task<CategoryDto?> GetAsync(Guid id)
    {
        Category? result = await _categoryRepository.GetAsync(id);

        return _mapper.Map<Category, CategoryDto>(result);
    }

    public async Task<DataResult<CategoryDto>> GetAsync()
    {
        IEnumerable<Category> result = await _categoryRepository.GetAsync();
        IEnumerable<CategoryDto> resultDto = _mapper.Map<Category, CategoryDto>(result);

        return new DataResult<CategoryDto>(resultDto);
    }

    public Task<bool> CreateAsync(NewCategoryDto categoryDto)
    {
        Category category = _mapper.Map<NewCategoryDto, Category>(categoryDto)!;

        return _categoryRepository.CreateAsync(category);
    }

    public Task<bool> UpdateAsync(Guid id, NewCategoryDto categoryDto)
    {
        Category category = _mapper.Map<NewCategoryDto, Category>(categoryDto)!;

        return _categoryRepository.UpdateAsync(id, category);
    }

    public Task DeleteAsync(Guid id)
    {
        return _categoryRepository.DeleteAsync(id);
    }
}
