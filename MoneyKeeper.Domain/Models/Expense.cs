using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyKeeper.Domain.Models;

[Table("expense")]
public sealed class Expense : BaseModel
{
    [Column("wallet_id")]
    public Guid WalletId { get; set; }

    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("date")]
    public long Date { get; set; }

    [Column("note"), StringLength(256)]
    public string? Note { get; set; }

    [Column("currency_id")]
    public Guid CurrencyId { get; set; }

    public Currency Currency { get; set; } = default!;

    [Column("category_id")]
    public Guid CategoryId { get; set; }

    public Category Category { get; set; } = default!;

    public Invoice? Invoice { get; set; }
}
