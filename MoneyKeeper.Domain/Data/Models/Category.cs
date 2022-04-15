using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Domain.Data.Models;

public sealed class Category : BaseModel
{
    [StringLength(maximumLength: 32)]
    public string Name { get; set; } = string.Empty;
    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
}
