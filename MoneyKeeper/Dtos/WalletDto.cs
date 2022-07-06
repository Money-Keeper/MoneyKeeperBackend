using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos;

public class WalletDto
{
    [Required]
    public Guid Id { get; set; }
}

public class NewWalletDto
{
    [Required]
    public Guid? UserId { get; set; }
}
