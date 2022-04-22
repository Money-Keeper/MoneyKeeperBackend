namespace MoneyKeeper.Domain.Data.Models;

public sealed class Expense : BaseModel
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Note { get; set; }
    public Guid CurrencyId { get; set; }
    public Currency Currency { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public Invoice? Invoice { get; set; }
}
