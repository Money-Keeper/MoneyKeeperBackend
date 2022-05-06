using MoneyKeeper.Domain.Constants;
using MoneyKeeper.Domain.Infrastructure.Commands;

namespace MoneyKeeper.Domain.Commands.FileCommands;

public sealed class CreateFileCommand : ICommand<CreateFileCommandResult>
{
    public CreateFileCommand(FileType fileType, ReadOnlyMemory<byte> file)
    {
        FileType = fileType;
        File = file;
    }

    public FileType FileType { get; }
    public ReadOnlyMemory<byte> File { get; }
}
