using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Registration.Dtos;

public sealed class RegistrationResponse
{
    [Required, StringLength(64, MinimumLength = 8)]
    public string? Login { get; set; }

    [Required]
    public string? Token { get; set; }
}
