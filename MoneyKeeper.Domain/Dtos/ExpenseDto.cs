using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Domain.Dtos;

public class ExpenseDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Note { get; set; }
}

public class NewExpenseDto
{
    [Required]
    public decimal? Amount { get; set; }

    [Required]
    public DateTime? Date { get; set; }

    public string? Note { get; set; }
}