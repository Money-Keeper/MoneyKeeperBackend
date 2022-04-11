using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Domain.Data.Models;

public sealed class Currency : BaseModel
{
    [StringLength(maximumLength: 32)]
    public string Name { get; set; } = string.Empty;

    [StringLength(maximumLength: 3)]
    public string Code { get; set; } = string.Empty;

    public char Symbol { get; set; }
}
