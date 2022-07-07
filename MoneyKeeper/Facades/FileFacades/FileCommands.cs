using MoneyKeeper.Domain.Commands.FileCommands;
using MoneyKeeper.Domain.Constants;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Facades.FileFacades.Abstractions;

namespace MoneyKeeper.Facades.FileFacades;

internal sealed class FileCommands : IFileCommands
{
    private readonly ICommandService<CreateFileCommand, CreateFileCommandResult> _createFileService;

    public FileCommands(ICommandService<CreateFileCommand, CreateFileCommandResult> createFileService)
    {
        _createFileService = createFileService ?? throw new ArgumentNullException(nameof(createFileService));
    }

    public async Task<string> CreateAsync(FileType fileType, ReadOnlyMemory<byte> file)
    {
        return (await _createFileService.ExecuteAsync(new CreateFileCommand(FileType.Pdf, file))).FileRelativePath;
    }
}
