using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyKeeper.Domain.Models;

[Table("invoice")]
public sealed class Invoice
{
    [Key(), Column("expense_id")]
    public Guid ExpenseId { get; set; }

    [Column("image_link"), StringLength(256)]
    public string ImageLink { get; set; } = string.Empty;

    [Column("pdf_link"), StringLength(256)]
    public string? PdfLink { get; set; }

    [Column("url"), StringLength(256)]
    public string? Url { get; set; }
}
