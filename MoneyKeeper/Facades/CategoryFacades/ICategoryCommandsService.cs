using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.CategoryFacades;

public interface ICategoryCommandsService
{
    Task<CategoryDto?> CreateAsync(NewCategoryDto newCategory);
    Task<CategoryDto?> UpdateAsync(Guid id, NewCategoryDto newCategory);
    Task DeleteAsync(Guid id);
}
