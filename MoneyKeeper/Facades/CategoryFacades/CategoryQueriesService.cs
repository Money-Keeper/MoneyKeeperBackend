using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries;
using MoneyKeeper.Domain.Queries.CategoryQueries;
using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.CategoryFacades;

public sealed class CategoryQueriesService : ICategoryQueriesService
{
    private readonly IMapper _mapper;
    private readonly IQueryService<EntityExistsQuery<Category>, bool> _categoryExistsQuery;
    private readonly IQueryService<GetCategoryByIdQuery, Category?> _getCategoryByIdQuery;
    private readonly IQueryService<GetCategoriesQuery, IEnumerable<Category>> _getCategoriesQuery;

    public CategoryQueriesService(
        IMapper mapper,
        IQueryService<EntityExistsQuery<Category>, bool> categoryExistsQuery,
        IQueryService<GetCategoryByIdQuery, Category?> getCategoryByIdQuery,
        IQueryService<GetCategoriesQuery, IEnumerable<Category>> getCategoriesQuery)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _categoryExistsQuery = categoryExistsQuery ?? throw new ArgumentNullException(nameof(categoryExistsQuery));
        _getCategoryByIdQuery = getCategoryByIdQuery ?? throw new ArgumentNullException(nameof(getCategoryByIdQuery));
        _getCategoriesQuery = getCategoriesQuery ?? throw new ArgumentNullException(nameof(getCategoriesQuery));
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _categoryExistsQuery.ExecuteAsync(new EntityExistsQuery<Category>(id));
    }

    public async Task<CategoryDto?> GetAsync(Guid id)
    {
        Category? result = await _getCategoryByIdQuery.ExecuteAsync(new GetCategoryByIdQuery(id));

        return _mapper.Map<Category, CategoryDto>(result);
    }

    public async Task<DataResult<CategoryDto>> GetAsync()
    {
        IEnumerable<Category> result = await _getCategoriesQuery.ExecuteAsync(new GetCategoriesQuery());
        IEnumerable<CategoryDto> resultDto = _mapper.Map<Category, CategoryDto>(result);

        return new DataResult<CategoryDto>(resultDto);
    }
}
