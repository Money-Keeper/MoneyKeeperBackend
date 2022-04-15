using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Domain.Dtos;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CategoryDto? ParentCategory { get; set; }
}

public class NewCategoryDto
{
    [Required]
    [StringLength(maximumLength: 32)]
    public string? Name { get; set; }
    public Guid? ParentCategoryId { get; set; }
}