namespace MoneyKeeper.Domain.Data.Models;

public sealed class Invoice : BaseModel
{
    public Guid ExpenseId { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public string? PdfPath { get; set; }
    public string? Url { get; set; }
}
