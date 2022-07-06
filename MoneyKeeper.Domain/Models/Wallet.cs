using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyKeeper.Domain.Models;

[Table("wallet")]
public sealed class Wallet : BaseModel
{
    [Column("user_id")]
    public Guid UserId { get; set; }
}
