using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyKeeper.Domain.Models;

public abstract class BaseModel
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("modified_at")]
    public DateTime? ModifiedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }
}
