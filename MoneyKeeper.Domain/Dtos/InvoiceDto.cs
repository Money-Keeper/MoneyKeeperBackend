using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Domain.Dtos;

public class InvoiceDto
{
    [Required]
    public string? ImagePath { get; set; }
    public string? PdfPath { get; set; }
    public string? Url { get; set; }
}
