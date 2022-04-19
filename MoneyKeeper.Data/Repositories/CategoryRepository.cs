using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Data.Abstractions.Repositories;
using MoneyKeeper.Domain.Data.Models;
using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Data.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly MoneyKeeperContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CategoryRepository(MoneyKeeperContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public Task<Category?> GetAsync(Guid id)
    {
        return _dbContext.Categories
            .AsNoTracking()
            .Include(x => x.ParentCategory)
            .Where(x => x.DeletedAt == null)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Category>> GetAsync()
    {
        return await _dbContext.Categories
            .AsNoTracking()
            .Include(x => x.ParentCategory)
            .Where(x => x.DeletedAt == null)
            .ToListAsync();
    }

    public async Task<bool> CreateAsync(Category category)
    {
        if (category.ParentCategoryId.HasValue)
        {
            bool isCategoryExists = await _dbContext.EntityExistsAsync<Category>(category.ParentCategoryId.Value);

            if (!isCategoryExists)
                return false;
        }

        _dbContext.Categories.Add(category);

        int affected = await _dbContext.SaveChangesAsync();

        return affected > 0;
    }

    public async Task<bool> UpdateAsync(Guid id, Category category)
    {
        if (category.ParentCategoryId.HasValue)
        {
            bool isCategoryExists = await _dbContext.EntityExistsAsync<Category>(category.ParentCategoryId.Value);

            if (!isCategoryExists)
                return false;
        }

        category.Id = id;
        category.ModifiedAt = _dateTimeProvider.NowUtc;

        _dbContext.Categories.Update(category);

        int affected = await _dbContext.SaveChangesAsync();

        return affected > 0;
    }

    public async Task DeleteAsync(Guid id)
    {
        Category category = _dbContext.Categories.Attach(new Category { Id = id }).Entity;

        category.DeletedAt = _dateTimeProvider.NowUtc;

        await _dbContext.SaveChangesAsync();
    }
}
