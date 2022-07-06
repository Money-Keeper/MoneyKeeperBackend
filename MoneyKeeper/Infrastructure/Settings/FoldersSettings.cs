using MoneyKeeper.Infrastructure.Settings.Abstractions;

namespace MoneyKeeper.Infrastructure.Settings;

internal sealed class FoldersSettings : ISettings<FoldersSettings>
{
    public string ImagesFolderName { get; set; } = default!;
    public string PdfFolderName { get; set; } = default!;

    public override string ToString() => nameof(FoldersSettings);
}
