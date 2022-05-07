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
    private readonly IQueryService<EntityExistsQuery<Category>, bool> _categoryExistsService;
    private readonly IQueryService<GetCategoryByIdQuery, Category?> _getCategoryByIdService;
    private readonly IQueryService<GetCategoriesQuery, IEnumerable<Category>> _getCategoriesService;

    public CategoryQueriesService(
        IMapper mapper,
        IQueryService<EntityExistsQuery<Category>, bool> categoryExistsQuery,
        IQueryService<GetCategoryByIdQuery, Category?> getCategoryByIdQuery,
        IQueryService<GetCategoriesQuery, IEnumerable<Category>> getCategoriesQuery)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _categoryExistsService = categoryExistsQuery ?? throw new ArgumentNullException(nameof(categoryExistsQuery));
        _getCategoryByIdService = getCategoryByIdQuery ?? throw new ArgumentNullException(nameof(getCategoryByIdQuery));
        _getCategoriesService = getCategoriesQuery ?? throw new ArgumentNullException(nameof(getCategoriesQuery));
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _categoryExistsService.ExecuteAsync(new EntityExistsQuery<Category>(id));
    }

    public async Task<CategoryDto?> GetAsync(Guid id)
    {
        Category? result = await _getCategoryByIdService.ExecuteAsync(new GetCategoryByIdQuery(id));

        return _mapper.Map<Category, CategoryDto>(result);
    }

    public async Task<DataResult<CategoryDto>> GetAsync()
    {
        IEnumerable<Category> result = await _getCategoriesService.ExecuteAsync(new GetCategoriesQuery());
        IEnumerable<CategoryDto> resultDto = _mapper.Map<Category, CategoryDto>(result);

        return new DataResult<CategoryDto>(resultDto);
    }
}
