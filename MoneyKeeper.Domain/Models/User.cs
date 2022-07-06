using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyKeeper.Domain.Models;

[Table("user")]
public sealed class User : BaseModel
{
    [Column("login"), StringLength(64)]
    public string Login { get; set; } = default!;

    [Column("name"), StringLength(64)]
    public string Name { get; set; } = default!;

    [Column("password_hash"), StringLength(60)]
    public string PasswordHash { get; set; } = default!;

    public IEnumerable<Wallet> Wallets { get; set; } = default!;
}
