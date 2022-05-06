using MoneyKeeper.Domain.Infrastructure.Commands;

namespace MoneyKeeper.Domain.Commands.FileCommands;

public sealed class CreateFileCommandResult : ICommandResult
{
    public CreateFileCommandResult(string fileRelativePath)
    {
        if (string.IsNullOrWhiteSpace(fileRelativePath))
            throw new ArgumentNullException(nameof(fileRelativePath));

        FileRelativePath = fileRelativePath;
    }

    public string FileRelativePath { get; }
}
