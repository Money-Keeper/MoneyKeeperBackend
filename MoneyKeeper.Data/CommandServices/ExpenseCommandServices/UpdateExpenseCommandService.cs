using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Commands.ExpenseCommands;
using MoneyKeeper.Domain.Constants;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Providers.Abstractions;
using MoneyKeeper.Domain.Queries.FileQueries;

namespace MoneyKeeper.Data.CommandServices.ExpenseCommandServices;

public sealed class UpdateExpenseCommandService : ICommandService<UpdateExpenseCommand, UpdateExpenseCommandResult>
{
    private class InvoiceUpdateResult
    {
        public string? OldImageLink { get; set; }
        public string? OldPdfLink { get; set; }
    }

    private readonly AppDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IQueryService<FileExistsQuery, bool> _fileExistsQuery;

    public UpdateExpenseCommandService(
        AppDbContext dbContext,
        IDateTimeProvider dateTimeProvider,
        IQueryService<FileExistsQuery, bool> fileExistsQuery)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        _fileExistsQuery = fileExistsQuery ?? throw new ArgumentNullException(nameof(fileExistsQuery));
    }

    public async Task<UpdateExpenseCommandResult> ExecuteAsync(UpdateExpenseCommand parameter)
    {
        Expense expense = parameter.NewExpense;

        bool nestedEntitiesExists = await NestedEntitiesExistsAsync(expense);

        if (!nestedEntitiesExists)
            return new UpdateExpenseCommandResult(null, null, null);

        if (expense.Invoice is not null)
        {
            bool filesExists = await FilesExistsAsync(expense.Invoice);

            if (!filesExists)
                return new UpdateExpenseCommandResult(null, null, null);
        }

        expense.Id = parameter.Id;
        expense.Date = expense.Date.ToUniversalTime();
        expense.ModifiedAt = _dateTimeProvider.NowUtc;

        InvoiceUpdateResult invoiceUpdateResult = await UpdateInvoiceAsync(expense);

        _dbContext.Update(expense);

        await _dbContext.SaveChangesAsync();

        return new UpdateExpenseCommandResult(parameter.Id, invoiceUpdateResult.OldImageLink, invoiceUpdateResult.OldPdfLink);
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

    private async Task<InvoiceUpdateResult> UpdateInvoiceAsync(Expense expense)
    {
        Invoice? existingInvoice = await _dbContext.Invoices
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ExpenseId == expense.Id);

        InvoiceUpdateResult result = new InvoiceUpdateResult();

        if (existingInvoice is null)
            return result;

        if (expense.Invoice is null)
        {
            result.OldImageLink = existingInvoice.ImageLink;
            result.OldPdfLink = existingInvoice.PdfLink;

            _dbContext.Remove(existingInvoice);

            return result;
        }

        if (existingInvoice.ImageLink != expense.Invoice.ImageLink)
        {
            result.OldImageLink = existingInvoice.ImageLink;
        }

        if (existingInvoice.PdfLink != expense.Invoice.PdfLink)
        {
            result.OldPdfLink = existingInvoice.PdfLink;
        }

        expense.Invoice.ExpenseId = expense.Id;

        _dbContext.Update(expense.Invoice);

        return result;
    }
}
