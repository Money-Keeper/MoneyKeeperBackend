using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyKeeper.Domain.Models;

[Table("wallet")]
public sealed class Wallet : BaseModel
{
    [Column("name"), StringLength(32)]
    public string Name { get; set; } = default!;

    [Column("user_id")]
    public Guid UserId { get; set; }
}
