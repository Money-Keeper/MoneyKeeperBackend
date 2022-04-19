namespace MoneyKeeper.Domain.Providers.FilesProvider;

public interface IFilesProvider
{
    bool Exists(string relativePath, FileType fileType);
    Task<byte[]> GetAsync(string relativePath, FileType fileType);
    Task<string> SaveAsync(ReadOnlyMemory<byte> file, FileType fileType);
    void Delete(string relativePath, FileType fileType);
}
