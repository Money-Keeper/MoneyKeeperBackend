using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Authentication.Dtos;

public sealed class AuthenticationResponse
{
    [Required, StringLength(64, MinimumLength = 8)]
    public string? Login { get; set; }

    [Required]
    public string? Token { get; set; }
}
