using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Domain.Dtos;

public class ExpenseDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Note { get; set; }
    public CurrencyDto Currency { get; set; } = null!;
    public CategoryDto Category { get; set; } = null!;
}

public class NewExpenseDto
{
    [Required]
    public decimal? Amount { get; set; }

    [Required]
    public DateTime? Date { get; set; }

    [Required]
    public Guid? CurrencyId { get; set; }

    [Required]
    public Guid? CategoryId { get; set; }

    public string? Note { get; set; }
}