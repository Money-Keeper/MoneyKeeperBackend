namespace MoneyKeeper.Domain.Exceptions;

public sealed class FileTypeException : ArgumentException
{
    private const string ExceptionMessage = "Unsupported file type";

    public FileTypeException(string? paramName) : base(ExceptionMessage, paramName)
    {
    }
}
