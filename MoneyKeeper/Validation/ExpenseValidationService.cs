using MoneyKeeper.Domain.Constants;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries;
using MoneyKeeper.Domain.Queries.FileQueries;
using MoneyKeeper.Dtos;
using MoneyKeeper.Validation.Abstractions;

namespace MoneyKeeper.Validation;

internal sealed class ExpenseValidationService : IValidationService<NewExpenseDto>
{
    private readonly IQueryService<EntityExistsQuery<Currency>, bool> _currencyExistsService;
    private readonly IQueryService<EntityExistsQuery<Category>, bool> _categoryExistsService;
    private readonly IQueryService<FileExistsQuery, bool> _fileExistsQuery;

    public ExpenseValidationService(
        IQueryService<EntityExistsQuery<Currency>, bool> currencyExistsService,
        IQueryService<EntityExistsQuery<Category>, bool> categoryExistsService,
        IQueryService<FileExistsQuery, bool> fileExistsQuery)
    {
        _currencyExistsService = currencyExistsService ?? throw new ArgumentNullException(nameof(currencyExistsService));
        _categoryExistsService = categoryExistsService ?? throw new ArgumentNullException(nameof(categoryExistsService));
        _fileExistsQuery = fileExistsQuery ?? throw new ArgumentNullException(nameof(fileExistsQuery));
    }

    public async Task<IValidationResult> ValidateAsync(NewExpenseDto dto)
    {
        var result = new ValidationResult();

        await Task.WhenAll(
            CheckCurrencyAsync(dto, result),
            CheckCategoryAsync(dto, result),
            CheckImageAsync(dto, result),
            CheckPdfAsync(dto, result));

        return result;
    }

    private async Task CheckCurrencyAsync(NewExpenseDto dto, ValidationResult result)
    {
        bool currencyExists = await _currencyExistsService.ExecuteAsync(new EntityExistsQuery<Currency>(dto.CurrencyId!.Value));

        if (!currencyExists)
        {
            result.AddError(nameof(dto.CurrencyId), "Currency does not exist");
        }
    }

    private async Task CheckCategoryAsync(NewExpenseDto dto, ValidationResult result)
    {
        bool categoryExists = await _categoryExistsService.ExecuteAsync(new EntityExistsQuery<Category>(dto.CategoryId!.Value));

        if (!categoryExists)
        {
            result.AddError(nameof(dto.CategoryId), "Category does not exist");
        }
    }

    private async Task CheckImageAsync(NewExpenseDto dto, ValidationResult result)
    {
        if (dto.Invoice is null)
            return;

        bool imageExists = await _fileExistsQuery.ExecuteAsync(new FileExistsQuery(FileType.Image, dto.Invoice.ImageLink!));

        if (!imageExists)
        {
            result.AddError(nameof(dto.Invoice.ImageLink), "Image does not exist");
        }
    }

    private async Task CheckPdfAsync(NewExpenseDto dto, ValidationResult result)
    {
        if (dto.Invoice is null || dto.Invoice.PdfLink is null)
            return;

        bool pdfExists = await _fileExistsQuery.ExecuteAsync(new FileExistsQuery(FileType.Pdf, dto.Invoice.PdfLink));

        if (!pdfExists)
        {
            result.AddError(nameof(dto.Invoice.PdfLink), "Pdf does not exist");
        }
    }
}
