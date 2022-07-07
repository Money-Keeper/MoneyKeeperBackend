using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Authentication.Dtos;

public sealed class AuthenticationRequest
{
    [Required, StringLength(64, MinimumLength = 8)]
    public string? Login { get; set; }

    [Required, StringLength(60, MinimumLength = 8)]
    public string? Password { get; set; }
}
