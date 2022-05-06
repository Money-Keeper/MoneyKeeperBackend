using MoneyKeeper.Domain.Commands;
using MoneyKeeper.Domain.Commands.FileCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Domain.CommandServices.FileCommandServices;

public sealed class DeleteFileCommandService : ICommandService<DeleteFileCommand, EmptyCommandResult>
{
    private readonly IFileDirectoryProvider _fileDirectoryProvider;

    public DeleteFileCommandService(IFileDirectoryProvider fileDirectoryProvider)
    {
        _fileDirectoryProvider = fileDirectoryProvider ?? throw new ArgumentNullException(nameof(fileDirectoryProvider));
    }

    public Task<EmptyCommandResult> ExecuteAsync(DeleteFileCommand parameter)
    {
        string rootDirectory = _fileDirectoryProvider[parameter.FileType];
        string fileAbsolutePath = Path.Combine(rootDirectory, parameter.FileRelativePath);

        File.Delete(fileAbsolutePath);

        return Task.FromResult(new EmptyCommandResult());
    }
}
