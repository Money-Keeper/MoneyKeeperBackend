using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyKeeper.Domain.Models;

[Table("currency")]
public sealed class Currency : BaseModel
{
    [Column("name"), StringLength(32)]
    public string Name { get; set; } = string.Empty;

    [Column("code"), StringLength(3)]
    public string Code { get; set; } = string.Empty;

    [Column("symbol"), StringLength(1)]
    public string Symbol { get; set; } = string.Empty;
}
