using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.FileFacades;

public interface IPdfService
{
    bool IsValidLink(string link);
    Task<bool> ExistsAsync(string link);
    Task<FileContentResult> GetAsync(string link);
    Task<FileLinkDto> CreateAsync(IFormFile file);
}
