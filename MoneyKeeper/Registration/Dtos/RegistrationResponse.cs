using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Registration.Dtos;

public sealed class RegistrationResponse
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string? Token { get; set; }
}
