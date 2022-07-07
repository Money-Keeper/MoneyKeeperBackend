using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries;
using MoneyKeeper.Domain.Queries.CategoryQueries;
using MoneyKeeper.Facades.CategoryFacades.Abstractions;

namespace MoneyKeeper.Facades.CategoryFacades;

internal sealed class CategoryQueries : ICategoryQueries
{
    private readonly IQueryService<EntityExistsQuery<Category>, bool> _categoryExistsService;
    private readonly IQueryService<GetCategoryByIdQuery, Category?> _getCategoryByIdService;
    private readonly IQueryService<GetCategoriesQuery, IEnumerable<Category>> _getCategoriesService;

    public CategoryQueries(
        IQueryService<EntityExistsQuery<Category>, bool> categoryExistsService,
        IQueryService<GetCategoryByIdQuery, Category?> getCategoryByIdService,
        IQueryService<GetCategoriesQuery, IEnumerable<Category>> getCategoriesService)
    {
        _categoryExistsService = categoryExistsService ?? throw new ArgumentNullException(nameof(categoryExistsService));
        _getCategoryByIdService = getCategoryByIdService ?? throw new ArgumentNullException(nameof(getCategoryByIdService));
        _getCategoriesService = getCategoriesService ?? throw new ArgumentNullException(nameof(getCategoriesService));
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _categoryExistsService.ExecuteAsync(new EntityExistsQuery<Category>(id));
    }

    public Task<Category?> GetAsync(Guid id)
    {
        return _getCategoryByIdService.ExecuteAsync(new GetCategoryByIdQuery(id));
    }

    public Task<IEnumerable<Category>> GetAsync()
    {
        return _getCategoriesService.ExecuteAsync(new GetCategoriesQuery());
    }
}
