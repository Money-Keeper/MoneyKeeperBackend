using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos;

public sealed class WalletDto
{
    [Required]
    public Guid Id { get; set; }

    [Required, StringLength(32, MinimumLength = 1)]
    public string? Name { get; set; }
}

public sealed class NewWalletDto
{
    [Required, StringLength(32, MinimumLength = 1)]
    public string? Name { get; set; }
}
