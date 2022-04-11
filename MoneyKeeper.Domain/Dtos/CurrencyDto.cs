using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Domain.Dtos;

public class CurrencyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public char Symbol { get; set; }
}

public class NewCurrencyDto
{
    [Required]
    [StringLength(maximumLength: 32)]
    public string? Name { get; set; }

    [Required]
    [StringLength(maximumLength: 3, MinimumLength = 3)]
    public string? Code { get; set; }

    [Required]
    public char? Symbol { get; set; }
}
