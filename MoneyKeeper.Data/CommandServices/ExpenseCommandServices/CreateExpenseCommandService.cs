using MoneyKeeper.Domain.Commands.ExpenseCommands;
using MoneyKeeper.Domain.Constants;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.FileQueries;

namespace MoneyKeeper.Data.CommandServices.ExpenseCommandServices;

public sealed class CreateExpenseCommandService : ICommandService<CreateExpenseCommand, CreateExpenseCommandResult>
{
    private readonly AppDbContext _dbContext;
    private readonly IQueryService<FileExistsQuery, bool> _fileExistsQuery;

    public CreateExpenseCommandService(AppDbContext dbContext, IQueryService<FileExistsQuery, bool> fileExistsQuery)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _fileExistsQuery = fileExistsQuery ?? throw new ArgumentNullException(nameof(fileExistsQuery));
    }

    public async Task<CreateExpenseCommandResult> ExecuteAsync(CreateExpenseCommand parameter)
    {
        Expense expense = parameter.NewExpense;

        bool nestedEntitiesExists = await NestedEntitiesExistsAsync(expense);

        if (!nestedEntitiesExists)
            return new CreateExpenseCommandResult(null);

        if (expense.Invoice is not null)
        {
            bool filesExists = await FilesExistsAsync(expense.Invoice);

            if (!filesExists)
                return new CreateExpenseCommandResult(null);
        }

        Guid result = _dbContext.Expenses.Add(expense).Entity.Id;

        await _dbContext.SaveChangesAsync();

        return new CreateExpenseCommandResult(result);
    }

    private async Task<bool> NestedEntitiesExistsAsync(Expense expense)
    {
        bool currencyExists = await _dbContext.EntityExistsAsync<Currency>(expense.CurrencyId);
        bool categoryExists = await _dbContext.EntityExistsAsync<Category>(expense.CategoryId);

        return currencyExists && categoryExists;
    }

    private async Task<bool> FilesExistsAsync(Invoice invoice)
    {
        bool imageExists = await _fileExistsQuery.ExecuteAsync(new FileExistsQuery(FileType.Image, invoice.ImageLink));

        if (invoice.PdfLink == null)
        {
            return imageExists;
        }

        bool pdfExists = await _fileExistsQuery.ExecuteAsync(new FileExistsQuery(FileType.Pdf, invoice.PdfLink));

        return imageExists && pdfExists;
    }
}
