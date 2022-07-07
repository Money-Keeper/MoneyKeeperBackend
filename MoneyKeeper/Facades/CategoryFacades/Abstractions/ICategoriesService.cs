using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.CategoryFacades.Abstractions;

public interface ICategoriesService
{
    Task<CategoryDto> CreateAsync(NewCategoryDto dto);
    Task<CategoryDto> UpdateAsync(Guid id, NewCategoryDto dto);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<CategoryDto?> GetAsync(Guid id);
    Task<DataResult<CategoryDto>> GetAsync();
}
