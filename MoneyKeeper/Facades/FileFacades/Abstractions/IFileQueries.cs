using MoneyKeeper.Domain.Constants;

namespace MoneyKeeper.Facades.FileFacades.Abstractions;

internal interface IFileQueries
{
    Task<bool> ExistsAsync(FileType fileType, string fileRelativePath);
    Task<byte[]> GetAsync(FileType fileType, string fileRelativePath);
}
