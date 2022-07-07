using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Dtos;
using MoneyKeeper.Validation.Abstractions;

namespace MoneyKeeper.Facades.FileFacades.Abstractions;

public interface IPdfService
{
    IValidationResult ValidateLink(string link);
    Task<bool> ExistsAsync(string link);
    Task<FileContentResult> GetAsync(string link);
    Task<FileLinkDto> CreateAsync(IFormFile file);
}
