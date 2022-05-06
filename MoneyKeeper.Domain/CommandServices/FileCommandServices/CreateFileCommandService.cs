using MoneyKeeper.Domain.Commands.FileCommands;
using MoneyKeeper.Domain.Constants;
using MoneyKeeper.Domain.Exceptions;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Domain.CommandServices.FileCommandServices;

public sealed class CreateFileCommandService : ICommandService<CreateFileCommand, CreateFileCommandResult>
{
    private const int SubfolderNameLength = 3;

    private readonly IFileDirectoryProvider _fileDirectoryProvider;
    private readonly IFileNameProvider _fileNameProvider;

    public CreateFileCommandService(IFileDirectoryProvider fileDirectoryProvider, IFileNameProvider fileNameProvider)
    {
        _fileDirectoryProvider = fileDirectoryProvider ?? throw new ArgumentNullException(nameof(fileDirectoryProvider));
        _fileNameProvider = fileNameProvider ?? throw new ArgumentNullException(nameof(fileNameProvider));
    }

    public async Task<CreateFileCommandResult> ExecuteAsync(CreateFileCommand parameter)
    {
        string rootDirectory = _fileDirectoryProvider[parameter.FileType];
        string fileName = _fileNameProvider.GetNewFileName() + GetFileExtension(parameter.FileType);

        string fileRelativePath = CreateFolder(rootDirectory, fileName);
        string fileAbsolutePath = Path.Combine(rootDirectory, fileRelativePath);

        using Stream fileStream = File.Create(fileAbsolutePath);

        await fileStream.WriteAsync(parameter.File);

        return new CreateFileCommandResult(fileRelativePath);
    }

    private string CreateFolder(string rootDirectory, string fileName)
    {
        string folderName = fileName[..SubfolderNameLength];
        string folderPath = Path.Combine(rootDirectory, folderName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        return Path.Combine(folderName, fileName);
    }

    private string GetFileExtension(FileType fileType) => fileType switch
    {
        FileType.Image => FileExtensions.Jpeg,
        FileType.Pdf => FileExtensions.Pdf,
        _ => throw new FileTypeException(nameof(fileType))
    };
}
