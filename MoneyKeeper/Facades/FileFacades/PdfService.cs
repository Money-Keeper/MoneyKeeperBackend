using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Domain.Constants;
using MoneyKeeper.Domain.Tools.Abstractions;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.FileFacades.Abstractions;
using MoneyKeeper.Validation;
using MoneyKeeper.Validation.Abstractions;
using System.Net.Mime;

namespace MoneyKeeper.Facades.FileFacades;

internal sealed class PdfService : IPdfService
{
    private readonly IPathConverter _pathConverter;
    private readonly IFileCommands _fileCommands;
    private readonly IFileQueries _fileQueries;

    public PdfService(IPathConverter pathConverter, IFileCommands fileCommands, IFileQueries fileQueries)
    {
        _pathConverter = pathConverter ?? throw new ArgumentNullException(nameof(pathConverter));
        _fileCommands = fileCommands ?? throw new ArgumentNullException(nameof(fileCommands));
        _fileQueries = fileQueries ?? throw new ArgumentNullException(nameof(fileQueries));
    }

    public async Task<FileLinkDto> CreateAsync(IFormFile file)
    {
        using Stream stream = file.OpenReadStream();
        byte[] pdf = new byte[stream.Length];

        await stream.ReadAsync(pdf);

        string result = await _fileCommands.CreateAsync(FileType.Pdf, pdf);
        return new FileLinkDto(_pathConverter.ToLink(result));
    }

    public Task<bool> ExistsAsync(string link)
    {
        return _fileQueries.ExistsAsync(FileType.Pdf, _pathConverter.FromLink(link));
    }

    public async Task<FileContentResult> GetAsync(string link)
    {
        byte[] result = await _fileQueries.GetAsync(FileType.Pdf, _pathConverter.FromLink(link));
        return new FileContentResult(result, MediaTypeNames.Application.Pdf);
    }

    public IValidationResult ValidateLink(string link)
    {
        var result = new ValidationResult();

        if (!IsValidLink(link))
        {
            result.AddError(nameof(link), "Invalid link");
        }

        return result;
    }

    private bool IsValidLink(string link)
    {
        return link.EndsWith(FileExtensions.Pdf);
    }
}
