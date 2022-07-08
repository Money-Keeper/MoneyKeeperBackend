using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos;

public class UserDto
{
    [Required]
    public Guid Id { get; set; }

    [Required, StringLength(64, MinimumLength = 8)]
    public string? Login { get; set; }

    [Required, StringLength(64, MinimumLength = 1)]
    public string? Name { get; set; }
}
