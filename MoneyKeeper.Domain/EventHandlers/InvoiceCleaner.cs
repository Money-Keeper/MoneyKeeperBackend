using MoneyKeeper.Domain.Commands;
using MoneyKeeper.Domain.Commands.FileCommands;
using MoneyKeeper.Domain.Constants;
using MoneyKeeper.Domain.Events;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Infrastructure.Events;
using MoneyKeeper.Domain.Tools.Abstractions;

namespace MoneyKeeper.Domain.EventHandlers;

public sealed class InvoiceCleaner : IAsyncEventHandler<ExpenseUpdatedEvent>
{
    private readonly IPathConverter _pathConverter;
    private readonly ICommandService<DeleteFileCommand, EmptyCommandResult> _deleteFileService;

    public InvoiceCleaner(
        IPathConverter pathConverter,
        ICommandService<DeleteFileCommand, EmptyCommandResult> deleteFileService)
    {
        _pathConverter = pathConverter ?? throw new ArgumentNullException(nameof(pathConverter));
        _deleteFileService = deleteFileService ?? throw new ArgumentNullException(nameof(deleteFileService));
    }

    public async Task HandleAsync(ExpenseUpdatedEvent e)
    {
        if (e.OldImageLink != null)
        {
            string fileRelativePath = _pathConverter.FromLink(e.OldImageLink);

            await _deleteFileService.ExecuteAsync(new DeleteFileCommand(FileType.Image, fileRelativePath));
        }

        if (e.OldPdfLink != null)
        {
            string fileRelativePath = _pathConverter.FromLink(e.OldPdfLink);

            await _deleteFileService.ExecuteAsync(new DeleteFileCommand(FileType.Pdf, fileRelativePath));
        }
    }
}
