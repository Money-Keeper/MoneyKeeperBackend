using MoneyKeeper.Domain.Constants;
using MoneyKeeper.Domain.Exceptions;
using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Domain.Providers;

public sealed class FileDirectoryProvider : IFileDirectoryProvider
{
    private readonly string _imagesDirectory;
    private readonly string _pdfDirectory;

    public FileDirectoryProvider(string imagesDirectory, string pdfDirectory)
    {
        if (string.IsNullOrWhiteSpace(imagesDirectory))
            throw new ArgumentNullException(nameof(imagesDirectory));

        if (string.IsNullOrWhiteSpace(pdfDirectory))
            throw new ArgumentNullException(nameof(pdfDirectory));

        _imagesDirectory = imagesDirectory;
        _pdfDirectory = pdfDirectory;
    }

    public string this[FileType fileType] => fileType switch
    {
        FileType.Image => _imagesDirectory,
        FileType.Pdf => _pdfDirectory,
        _ => throw new FileTypeException(nameof(fileType))
    };

    public string GetDirectory(FileType fileType) => this[fileType];
}
