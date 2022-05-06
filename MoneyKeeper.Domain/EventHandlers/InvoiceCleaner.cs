using MoneyKeeper.Domain.Commands;
using MoneyKeeper.Domain.Commands.FileCommands;
using MoneyKeeper.Domain.Constants;
using MoneyKeeper.Domain.Events;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Infrastructure.Events;
using MoneyKeeper.Domain.Tools.Abstractions;

namespace MoneyKeeper.Domain.EventHandlers;

public sealed class InvoiceCleaner : IAsyncEventHandler<ExpenseUpdatedEvent, EmptyEventResult>
{
    private readonly IPathConverter _pathConverter;
    private readonly ICommandService<DeleteFileCommand, EmptyCommandResult> _deleteFileCommand;

    public InvoiceCleaner(
        IPathConverter pathConverter,
        ICommandService<DeleteFileCommand, EmptyCommandResult> deleteFileCommand)
    {
        _pathConverter = pathConverter ?? throw new ArgumentNullException(nameof(pathConverter));
        _deleteFileCommand = deleteFileCommand ?? throw new ArgumentNullException(nameof(deleteFileCommand));
    }

    public async Task<EmptyEventResult> Handle(ExpenseUpdatedEvent e)
    {
        if (e.OldImageLink != null)
        {
            string fileRelativePath = _pathConverter.FromLink(e.OldImageLink);

            await _deleteFileCommand.ExecuteAsync(new DeleteFileCommand(FileType.Image, fileRelativePath));
        }

        if (e.OldPdfLink != null)
        {
            string fileRelativePath = _pathConverter.FromLink(e.OldPdfLink);

            await _deleteFileCommand.ExecuteAsync(new DeleteFileCommand(FileType.Pdf, fileRelativePath));
        }

        return new EmptyEventResult();
    }
}
