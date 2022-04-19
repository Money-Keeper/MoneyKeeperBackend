namespace MoneyKeeper.Domain.Providers.FilesProvider;

public sealed class FilesProvider : IFilesProvider
{
    private const string ExceptionMessage = "Unsupported file type";

    private readonly IReadOnlyDictionary<FileType, FilesProviderBase> _providers;

    public FilesProvider(IEnumerable<FilesProviderBase> providers)
    {
        if (providers is null)
            throw new ArgumentNullException(nameof(providers));

        _providers = providers.ToDictionary(x => x.FileType);
    }

    public bool Exists(string relativePath, FileType fileType) => fileType switch
    {
        FileType.Image => _providers[FileType.Image].Exists(relativePath),
        FileType.Pdf => _providers[FileType.Pdf].Exists(relativePath),
        _ => throw new ArgumentException(ExceptionMessage, nameof(fileType))
    };

    public Task<byte[]> GetAsync(string relativePath, FileType fileType) => fileType switch
    {
        FileType.Image => _providers[FileType.Image].GetAsync(relativePath),
        FileType.Pdf => _providers[FileType.Pdf].GetAsync(relativePath),
        _ => throw new ArgumentException(ExceptionMessage, nameof(fileType))
    };

    public Task<string> SaveAsync(ReadOnlyMemory<byte> file, FileType fileType) => fileType switch
    {
        FileType.Image => _providers[FileType.Image].SaveAsync(file),
        FileType.Pdf => _providers[FileType.Pdf].SaveAsync(file),
        _ => throw new ArgumentException(ExceptionMessage, nameof(fileType))
    };

    public void Delete(string relativePath, FileType fileType)
    {
        switch (fileType)
        {
            case FileType.Image:
                _providers[FileType.Image].Delete(relativePath);
                break;
            case FileType.Pdf:
                _providers[FileType.Pdf].Delete(relativePath);
                break;
            default:
                throw new ArgumentException(ExceptionMessage, nameof(fileType));
        }
    }
}
