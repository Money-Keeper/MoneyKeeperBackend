using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos;

public class RegistrationRequest
{
    [Required, StringLength(64, MinimumLength = 8)]
    public string? Login { get; set; }

    [Required, StringLength(64, MinimumLength = 1)]
    public string? Name { get; set; }

    [Required, StringLength(60, MinimumLength = 8)]
    public string? Password { get; set; }
}

public class RegistrationResponse
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string Token { get; set; } = default!;
}