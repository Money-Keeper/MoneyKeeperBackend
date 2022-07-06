using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos;

public class ExpenseDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public long Date { get; set; }

    [StringLength(256)]
    public string? Note { get; set; }

    [Required]
    public CurrencyDto Currency { get; set; } = default!;

    [Required]
    public CategoryDto Category { get; set; } = default!;

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

    [StringLength(256)]
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