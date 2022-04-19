namespace MoneyKeeper.Domain.Dtos;

public class FilePathDto
{
    public FilePathDto(string filePath)
    {
        FilePath = filePath;
    }

    public string FilePath { get; }
}
