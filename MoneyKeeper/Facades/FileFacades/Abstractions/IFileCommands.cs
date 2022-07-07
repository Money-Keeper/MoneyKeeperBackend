using MoneyKeeper.Domain.Constants;

namespace MoneyKeeper.Facades.FileFacades.Abstractions;

internal interface IFileCommands
{
    Task<string> CreateAsync(FileType fileType, ReadOnlyMemory<byte> file);
}
