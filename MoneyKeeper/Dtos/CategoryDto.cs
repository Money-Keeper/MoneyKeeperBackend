using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos;

public class CategoryDto
{
    [Required]
    public Guid Id { get; set; }

    [Required, StringLength(32, MinimumLength = 1)]
    public string? Name { get; set; }

    [Required, StringLength(6, MinimumLength = 6)]
    public string? Color { get; set; }

    public CategoryDto? ParentCategory { get; set; }
}

public class NewCategoryDto
{
    [Required, StringLength(32, MinimumLength = 1)]
    public string? Name { get; set; }

    [Required, StringLength(6, MinimumLength = 6)]
    public string? Color { get; set; }

    public Guid? ParentCategoryId { get; set; }
}