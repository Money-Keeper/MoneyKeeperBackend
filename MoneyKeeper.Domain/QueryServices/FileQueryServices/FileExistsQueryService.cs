using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Providers.Abstractions;
using MoneyKeeper.Domain.Queries.FileQueries;

namespace MoneyKeeper.Domain.QueryServices.FileQueryServices;

public sealed class FileExistsQueryService : IQueryService<FileExistsQuery, bool>
{
    private readonly IFileDirectoryProvider _fileDirectoryProvider;

    public FileExistsQueryService(IFileDirectoryProvider fileDirectoryProvider)
    {
        _fileDirectoryProvider = fileDirectoryProvider ?? throw new ArgumentNullException(nameof(fileDirectoryProvider));
    }

    public Task<bool> ExecuteAsync(FileExistsQuery parameter)
    {
        string absolutePath = Path.Combine(_fileDirectoryProvider[parameter.FileType], parameter.FileRelativePath);

        var result = File.Exists(absolutePath);

        return Task.FromResult(result);
    }
}
