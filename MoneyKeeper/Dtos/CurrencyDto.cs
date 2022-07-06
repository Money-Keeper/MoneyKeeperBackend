using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos;

public class CurrencyDto
{
    [Required]
    public Guid Id { get; set; }

    [Required, StringLength(32, MinimumLength = 1)]
    public string Name { get; set; } = default!;

    [Required, StringLength(3, MinimumLength = 3)]
    public string Code { get; set; } = default!;

    [Required, StringLength(1, MinimumLength = 1)]
    public string Symbol { get; set; } = default!;
}

public class NewCurrencyDto
{
    [Required, StringLength(32, MinimumLength = 1)]
    public string? Name { get; set; }

    [Required, StringLength(3, MinimumLength = 3)]
    public string? Code { get; set; }

    [Required, StringLength(1, MinimumLength = 1)]
    public string? Symbol { get; set; }
}
