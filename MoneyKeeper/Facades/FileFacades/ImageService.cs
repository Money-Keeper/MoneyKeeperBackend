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

public sealed class ImageService : IImageService
{
    private readonly IQueryService<FileExistsQuery, bool> _fileExistsService;
    private readonly IQueryService<GetFileQuery, byte[]> _getFileService;
    private readonly ICommandService<CreateFileCommand, CreateFileCommandResult> _createFileService;
    private readonly IPathConverter _pathConverter;

    public ImageService(
        IQueryService<FileExistsQuery, bool> fileExistsService,
        IQueryService<GetFileQuery, byte[]> getFileService,
        ICommandService<CreateFileCommand, CreateFileCommandResult> createFileService,
        IPathConverter pathConverter)
    {
        _fileExistsService = fileExistsService ?? throw new ArgumentNullException(nameof(fileExistsService));
        _getFileService = getFileService ?? throw new ArgumentNullException(nameof(getFileService));
        _createFileService = createFileService ?? throw new ArgumentNullException(nameof(createFileService));
        _pathConverter = pathConverter ?? throw new ArgumentNullException(nameof(pathConverter));
    }

    public async Task<FileLinkDto> CreateAsync(IFormFile file)
    {
        using Stream stream = file.OpenReadStream();

        byte[] image = new byte[stream.Length];

        await stream.ReadAsync(image);

        CreateFileCommandResult result = await _createFileService.ExecuteAsync(new CreateFileCommand(FileType.Image, image));

        return new FileLinkDto(_pathConverter.ToLink(result.FileRelativePath));
    }

    public Task<bool> ExistsAsync(string link)
    {
        string path = _pathConverter.FromLink(link);

        return _fileExistsService.ExecuteAsync(new FileExistsQuery(FileType.Image, path));
    }

    public async Task<FileContentResult> GetAsync(string link)
    {
        string path = _pathConverter.FromLink(link);

        byte[] file = await _getFileService.ExecuteAsync(new GetFileQuery(FileType.Image, path));

        return new FileContentResult(file, MediaTypeNames.Image.Jpeg);
    }

    public bool IsValidLink(string link)
    {
        return link.EndsWith(FileExtensions.Jpg) || link.EndsWith(FileExtensions.Jpeg);
    }
}
