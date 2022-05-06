using MoneyKeeper.Domain.Providers;
using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Factories;

internal class FileDirectoryProviderFactory
{
    public IFileDirectoryProvider Create(IConfiguration appConfig)
    {
        ArgumentNullException.ThrowIfNull(appConfig, nameof(appConfig));

        const string FolderNames = nameof(FolderNames);
        const string ImagesFolder = nameof(ImagesFolder);
        const string PdfFolder = nameof(PdfFolder);

        IConfigurationSection section = appConfig.GetRequiredSection(FolderNames);
        string imagesRootDirectory = Path.Combine(AppContext.BaseDirectory, section.GetRequiredSection(ImagesFolder).Value);
        string pdfRootDirectory = Path.Combine(AppContext.BaseDirectory, section.GetRequiredSection(PdfFolder).Value);

        return new FileDirectoryProvider(imagesRootDirectory, pdfRootDirectory);
    }
}
