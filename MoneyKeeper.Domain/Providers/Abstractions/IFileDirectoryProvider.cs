using MoneyKeeper.Domain.Constants;

namespace MoneyKeeper.Domain.Providers.Abstractions;

public interface IFileDirectoryProvider
{
    string this[FileType fileType] { get; }
    string GetDirectory(FileType fileType);
}
