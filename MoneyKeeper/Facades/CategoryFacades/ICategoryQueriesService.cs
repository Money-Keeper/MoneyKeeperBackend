using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.CategoryFacades;

public interface ICategoryQueriesService
{
    Task<bool> ExistsAsync(Guid id);
    Task<CategoryDto?> GetAsync(Guid id);
    Task<DataResult<CategoryDto>> GetAsync();
}
