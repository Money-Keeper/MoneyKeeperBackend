using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos;

public class ExpenseDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public long Date { get; set; }
    public string? Note { get; set; }
    public CurrencyDto Currency { get; set; } = null!;
    public CategoryDto Category { get; set; } = null!;
    public InvoiceDto? Invoice { get; set; }
}

public class NewExpenseDto
{
    [Required]
    public decimal? Amount { get; set; }

    [Required]
    public long? Date { get; set; }

    [Required]
    public Guid? CurrencyId { get; set; }

    [Required, BindProperty(Name = "categoryId")]
    public Guid? CategoryId { get; set; }

    public string? Note { get; set; }

    public InvoiceDto? Invoice { get; set; }
}

public class ExpenseConditionDto
{
    [Required, FromQuery(Name = "categoryId")]
    public Guid? CategoryId { get; set; }

    [FromQuery(Name = "dateFrom")]
    public long? DateFrom { get; set; }

    [FromQuery(Name = "dateTo")]
    public long? DateTo { get; set; }
}