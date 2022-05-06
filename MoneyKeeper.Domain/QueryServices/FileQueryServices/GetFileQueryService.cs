using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Providers.Abstractions;
using MoneyKeeper.Domain.Queries.FileQueries;

namespace MoneyKeeper.Domain.QueryServices.FileQueryServices;

public sealed class GetFileQueryService : IQueryService<GetFileQuery, byte[]>
{
    private readonly IFileDirectoryProvider _fileDirectoryProvider;

    public GetFileQueryService(IFileDirectoryProvider fileDirectoryProvider)
    {
        _fileDirectoryProvider = fileDirectoryProvider ?? throw new ArgumentNullException(nameof(fileDirectoryProvider));
    }

    public async Task<byte[]> ExecuteAsync(GetFileQuery parameter)
    {
        string absolutePath = Path.Combine(_fileDirectoryProvider[parameter.FileType], parameter.FileRelativePath);

        using Stream fileStream = File.OpenRead(absolutePath);

        byte[] file = new byte[fileStream.Length];

        await fileStream.ReadAsync(file);

        return file;
    }
}
