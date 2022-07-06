using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos;

public class AuthenticationRequest
{
    [Required, StringLength(64, MinimumLength = 8)]
    public string? Login { get; set; }

    [Required, StringLength(60, MinimumLength = 8)]
    public string? Password { get; set; }
}

public class AuthenticationResponse
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string Token { get; set; } = default!;
}
