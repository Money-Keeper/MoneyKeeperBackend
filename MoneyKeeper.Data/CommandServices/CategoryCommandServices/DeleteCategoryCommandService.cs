using MoneyKeeper.Domain.Commands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Data.CommandServices.CategoryCommandServices;

public sealed class DeleteCategoryCommandService : ICommandService<DeleteEntityCommand<Category>, EmptyCommandResult>
{
    private readonly AppDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public DeleteCategoryCommandService(AppDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public async Task<EmptyCommandResult> ExecuteAsync(DeleteEntityCommand<Category> parameter)
    {
        Category category = _dbContext.Categories.Attach(new Category { Id = parameter.Id }).Entity;

        category.DeletedAt = _dateTimeProvider.NowUtc;

        await _dbContext.SaveChangesAsync();

        return new EmptyCommandResult();
    }
}
