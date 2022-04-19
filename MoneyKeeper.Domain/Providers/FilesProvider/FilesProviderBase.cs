namespace MoneyKeeper.Domain.Providers.FilesProvider;

public abstract class FilesProviderBase
{
    private const int SubfolderNameLenght = 3;

    protected readonly string _rootDirectoryPath;

    public FilesProviderBase(string rootDirectoryPath)
    {
        if (string.IsNullOrWhiteSpace(rootDirectoryPath))
            throw new ArgumentNullException(nameof(rootDirectoryPath));

        _rootDirectoryPath = rootDirectoryPath;
    }

    public abstract FileType FileType { get; }

    public bool Exists(string relativePath)
    {
        string absolutePath = Path.Combine(_rootDirectoryPath, relativePath);

        return File.Exists(absolutePath);
    }

    public void Delete(string relativePath)
    {
        string absolutePath = Path.Combine(_rootDirectoryPath, relativePath);

        File.Delete(absolutePath);
    }

    public abstract Task<byte[]> GetAsync(string fileName);
    public abstract Task<string> SaveAsync(ReadOnlyMemory<byte> file);

    protected string GetFileRelativePath(string fileName)
    {
        string folderName = fileName[..SubfolderNameLenght];
        string directoryPath = Path.Combine(_rootDirectoryPath, folderName);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        return Path.Combine(folderName, fileName);
    }
}
