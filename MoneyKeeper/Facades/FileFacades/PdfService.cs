using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Domain.Commands.FileCommands;
using MoneyKeeper.Domain.Constants;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Queries.FileQueries;
using MoneyKeeper.Domain.Tools.Abstractions;
using MoneyKeeper.Dtos;
using System.Net.Mime;

namespace MoneyKeeper.Facades.FileFacades;

public sealed class PdfService : IPdfService
{
    private readonly IQueryService<FileExistsQuery, bool> _fileExistsQuery;
    private readonly IQueryService<GetFileQuery, byte[]> _getFileQuery;
    private readonly ICommandService<CreateFileCommand, CreateFileCommandResult> _createFileCommand;
    private readonly IPathConverter _pathConverter;

    public PdfService(
        IQueryService<FileExistsQuery, bool> fileExistsQuery,
        IQueryService<GetFileQuery, byte[]> getFileQuery,
        ICommandService<CreateFileCommand, CreateFileCommandResult> createFileCommand,
        IPathConverter pathConverter)
    {
        _fileExistsQuery = fileExistsQuery ?? throw new ArgumentNullException(nameof(fileExistsQuery));
        _getFileQuery = getFileQuery ?? throw new ArgumentNullException(nameof(getFileQuery));
        _createFileCommand = createFileCommand ?? throw new ArgumentNullException(nameof(createFileCommand));
        _pathConverter = pathConverter ?? throw new ArgumentNullException(nameof(pathConverter));
    }

    public async Task<FileLinkDto> CreateAsync(IFormFile file)
    {
        using Stream stream = file.OpenReadStream();

        byte[] pdf = new byte[stream.Length];

        await stream.ReadAsync(pdf);

        CreateFileCommandResult result = await _createFileCommand.ExecuteAsync(new CreateFileCommand(FileType.Pdf, pdf));

        return new FileLinkDto(_pathConverter.ToLink(result.FileRelativePath));
    }

    public Task<bool> ExistsAsync(string link)
    {
        string path = _pathConverter.FromLink(link);

        return _fileExistsQuery.ExecuteAsync(new FileExistsQuery(FileType.Pdf, path));
    }

    public async Task<FileContentResult> GetAsync(string link)
    {
        string path = _pathConverter.FromLink(link);

        byte[] file = await _getFileQuery.ExecuteAsync(new GetFileQuery(FileType.Pdf, path));

        return new FileContentResult(file, MediaTypeNames.Application.Pdf);
    }

    public bool IsValidLink(string link)
    {
        return link.EndsWith(FileExtensions.Pdf);
    }
}
