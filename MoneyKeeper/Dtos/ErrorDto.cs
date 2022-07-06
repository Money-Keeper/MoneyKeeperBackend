namespace MoneyKeeper.Dtos;

public class ErrorDto
{
    public ErrorDto(string title, int statusCode, string? message = null)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        StatusCode = statusCode;
        Message = message;
    }

    public string Title { get; }
    public int StatusCode { get; }
    public string? Message { get; }
}
