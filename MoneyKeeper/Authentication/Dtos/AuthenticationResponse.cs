using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Authentication.Dtos;

public sealed class AuthenticationResponse
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string? Token { get; set; }
}
