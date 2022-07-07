using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries;
using MoneyKeeper.Dtos;
using MoneyKeeper.Validation.Abstractions;
using System.Globalization;

namespace MoneyKeeper.Validation;

internal sealed class CategoryValidationService : IValidationService<NewCategoryDto>
{
    private readonly IQueryService<EntityExistsQuery<Category>, bool> _categoryExistsService;

    public CategoryValidationService(IQueryService<EntityExistsQuery<Category>, bool> categoryExistsService)
    {
        _categoryExistsService = categoryExistsService ?? throw new ArgumentNullException(nameof(categoryExistsService));
    }

    public async Task<IValidationResult> ValidateAsync(NewCategoryDto dto)
    {
        var result = new ValidationResult();

        await Task.WhenAll(CheckColorAsync(dto, result), CheckCategoryAsync(dto, result));

        return result;
    }

    private Task CheckColorAsync(NewCategoryDto dto, ValidationResult result)
    {
        if (!IsHex(dto.Color!))
        {
            result.AddError(nameof(dto.Color), "Color must be in hex format");
        }

        return Task.CompletedTask;
    }

    private async Task CheckCategoryAsync(NewCategoryDto dto, ValidationResult result)
    {
        if (dto.ParentCategoryId == null)
            return;

        bool categoryExits = await _categoryExistsService.ExecuteAsync(new EntityExistsQuery<Category>(dto.ParentCategoryId.Value));

        if (!categoryExits)
        {
            result.AddError(nameof(dto.ParentCategoryId), "Parent category does not exist");
        }
    }

    private bool IsHex(string str)
    {
        return int.TryParse(str, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out _);
    }
}
