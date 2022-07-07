using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Registration.Dtos;

public sealed class RegistrationRequest
{
    [Required, StringLength(64, MinimumLength = 8)]
    public string? Login { get; set; }

    [Required, StringLength(64, MinimumLength = 1)]
    public string? Name { get; set; }

    [Required, StringLength(60, MinimumLength = 8)]
    public string? Password { get; set; }
}