using MoneyKeeper.Domain.Dtos;

namespace MoneyKeeper.Domain.Services.Abstractions;

public interface ICategoryService
{
    Task<bool> IsExistsAsync(Guid id);
    Task<CategoryDto?> GetAsync(Guid id);
    Task<DataResult<CategoryDto>> GetAsync();
    Task<bool> CreateAsync(NewCategoryDto categoryDto);
    Task<bool> UpdateAsync(Guid id, NewCategoryDto categoryDto);
    Task DeleteAsync(Guid id);
}
