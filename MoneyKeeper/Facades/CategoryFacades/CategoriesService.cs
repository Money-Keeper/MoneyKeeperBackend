using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.CategoryFacades.Abstractions;

namespace MoneyKeeper.Facades.CategoryFacades;

internal sealed class CategoriesService : ICategoriesService
{
    private readonly IMapper _mapper;
    private readonly ICategoryCommands _categoryCommands;
    private readonly ICategoryQueries _categoryQueries;

    public CategoriesService(IMapper mapper, ICategoryCommands categoryCommands, ICategoryQueries categoryQueries)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _categoryCommands = categoryCommands ?? throw new ArgumentNullException(nameof(categoryCommands));
        _categoryQueries = categoryQueries ?? throw new ArgumentNullException(nameof(categoryQueries));
    }

    public async Task<CategoryDto> CreateAsync(NewCategoryDto dto)
    {
        Category result = await _categoryCommands.CreateAsync(_mapper.Map<NewCategoryDto, Category>(dto)!);
        return _mapper.Map<Category, CategoryDto>(result)!;
    }

    public Task DeleteAsync(Guid id) => _categoryCommands.DeleteAsync(id);

    public Task<bool> ExistsAsync(Guid id) => _categoryQueries.ExistsAsync(id);

    public async Task<CategoryDto?> GetAsync(Guid id)
    {
        Category? result = await _categoryQueries.GetAsync(id);
        return _mapper.Map<Category, CategoryDto>(result);
    }

    public async Task<DataResult<CategoryDto>> GetAsync()
    {
        IEnumerable<Category> result = await _categoryQueries.GetAsync();
        return new DataResult<CategoryDto>(_mapper.Map<Category, CategoryDto>(result)!);
    }

    public async Task<CategoryDto> UpdateAsync(Guid id, NewCategoryDto dto)
    {
        var result = await _categoryCommands.UpdateAsync(id, _mapper.Map<NewCategoryDto, Category>(dto)!);
        return _mapper.Map<Category, CategoryDto>(result)!;
    }
}
