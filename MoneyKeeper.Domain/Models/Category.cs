using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyKeeper.Domain.Models;

[Table("category")]
public sealed class Category : BaseModel
{
    [Column("name"), StringLength(32)]
    public string Name { get; set; } = string.Empty;

    [Column("color"), StringLength(6)]
    public string Color { get; set; } = string.Empty;

    [Column("parent_category_id")]
    public Guid? ParentCategoryId { get; set; }

    [Column("parent_category")]
    public Category? ParentCategory { get; set; }
}
