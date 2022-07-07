using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Domain.Commands.ExpenseCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Providers.Abstractions;

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

    public UpdateExpenseCommandService(AppDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public async Task<UpdateExpenseCommandResult> ExecuteAsync(UpdateExpenseCommand parameter)
    {
        Expense expense = parameter.NewExpense;

        expense.Id = parameter.Id;
        expense.ModifiedAt = _dateTimeProvider.NowUtc;

        InvoiceUpdateResult invoiceUpdateResult = await UpdateInvoiceAsync(expense);

        _dbContext.Update(expense);

        await _dbContext.SaveChangesAsync();

        return new UpdateExpenseCommandResult(parameter.Id, invoiceUpdateResult.OldImageLink, invoiceUpdateResult.OldPdfLink);
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

        return result;
    }
}
