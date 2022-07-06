using MoneyKeeper.Domain.Providers;
using MoneyKeeper.Domain.Providers.Abstractions;
using MoneyKeeper.Infrastructure.Settings;
using MoneyKeeper.Services.Abstractions;

namespace MoneyKeeper.Factories;

internal sealed class FileDirectoryProviderFactory
{
    public IFileDirectoryProvider Create(ISettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService, nameof(settingsService));

        FoldersSettings settings = settingsService.GetSettings<FoldersSettings>();
        string imagesRootDirectory = Path.Combine(AppContext.BaseDirectory, settings.ImagesFolderName);
        string pdfRootDirectory = Path.Combine(AppContext.BaseDirectory, settings.PdfFolderName);

        return new FileDirectoryProvider(imagesRootDirectory, pdfRootDirectory);
    }
}
