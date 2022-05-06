using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos;

public class InvoiceDto
{
    [Required, StringLength(256)]
    public string? ImageLink { get; set; }

    [StringLength(256)]
    public string? PdfLink { get; set; }

    [StringLength(256)]
    public string? Url { get; set; }
}
