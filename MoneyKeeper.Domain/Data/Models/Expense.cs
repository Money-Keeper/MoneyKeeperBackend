namespace MoneyKeeper.Domain.Data.Models;

public class Expense : BaseModel
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Note { get; set; }
}
