using MoneyKeeper.Domain.Constants;
using MoneyKeeper.Domain.Infrastructure.Commands;

namespace MoneyKeeper.Domain.Commands.FileCommands;

public sealed class DeleteFileCommand : ICommand<EmptyCommandResult>
{
    public DeleteFileCommand(FileType fileType, string fileRelativePath)
    {
        if (string.IsNullOrWhiteSpace(fileRelativePath))
            throw new ArgumentNullException(nameof(fileRelativePath));

        FileType = fileType;
        FileRelativePath = fileRelativePath;
    }

    public FileType FileType { get; }
    public string FileRelativePath { get; }
}
