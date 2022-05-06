using MoneyKeeper.Domain.Constants;
using MoneyKeeper.Domain.Infrastructure.Queries;

namespace MoneyKeeper.Domain.Queries.FileQueries;

public sealed class GetFileQuery : IQuery<byte[]>
{
    public GetFileQuery(FileType fileType, string fileRelativePath)
    {
        if (string.IsNullOrWhiteSpace(fileRelativePath))
            throw new ArgumentNullException(nameof(fileRelativePath));

        FileType = fileType;
        FileRelativePath = fileRelativePath;
    }

    public FileType FileType { get; }
    public string FileRelativePath { get; }
}
