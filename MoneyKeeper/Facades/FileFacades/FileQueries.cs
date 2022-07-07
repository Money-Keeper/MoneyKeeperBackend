using MoneyKeeper.Domain.Constants;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Queries.FileQueries;
using MoneyKeeper.Facades.FileFacades.Abstractions;

namespace MoneyKeeper.Facades.FileFacades;

internal sealed class FileQueries : IFileQueries
{
    private readonly IQueryService<FileExistsQuery, bool> _fileExistsService;
    private readonly IQueryService<GetFileQuery, byte[]> _getFileService;

    public FileQueries(IQueryService<FileExistsQuery, bool> fileExistsService, IQueryService<GetFileQuery, byte[]> getFileService)
    {
        _fileExistsService = fileExistsService ?? throw new ArgumentNullException(nameof(fileExistsService));
        _getFileService = getFileService ?? throw new ArgumentNullException(nameof(getFileService));
    }

    public Task<bool> ExistsAsync(FileType fileType, string fileRelativePath)
    {
        return _fileExistsService.ExecuteAsync(new FileExistsQuery(fileType, fileRelativePath));
    }

    public Task<byte[]> GetAsync(FileType fileType, string fileRelativePath)
    {
        return _getFileService.ExecuteAsync(new GetFileQuery(fileType, fileRelativePath));
    }
}
