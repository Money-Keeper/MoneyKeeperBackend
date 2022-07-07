using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Controllers.Abstractions;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.CategoryFacades.Abstractions;
using MoneyKeeper.Validation.Abstractions;

namespace MoneyKeeper.Controllers;

[Route("api/categories")]
public sealed class CategoriesController : BaseController
{
    private readonly IValidationService<NewCategoryDto> _validationService;
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(IValidationService<NewCategoryDto> validationService, ICategoriesService categoriesService)
    {
        _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
        _categoriesService = categoriesService ?? throw new ArgumentNullException(nameof(categoriesService));
    }

    [HttpGet]
    public Task<DataResult<CategoryDto>> Get() => _categoriesService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> Get(Guid id)
    {
        CategoryDto? result = await _categoriesService.GetAsync(id);

        if (result is null)
            return NotFound();

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Post(NewCategoryDto newCategoryDto)
    {
        IValidationResult validationResult = await _validationService.ValidateAsync(newCategoryDto);

        if (validationResult.IsFailed)
            return BadRequest(validationResult);

        return await _categoriesService.CreateAsync(newCategoryDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDto>> Put(Guid id, NewCategoryDto newCategoryDto)
    {
        bool exists = await _categoriesService.ExistsAsync(id);

        if (!exists)
            return NotFound();

        IValidationResult validationResult = await _validationService.ValidateAsync(newCategoryDto);

        if (validationResult.IsFailed)
            return BadRequest(validationResult);

        return await _categoriesService.UpdateAsync(id, newCategoryDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool exists = await _categoriesService.ExistsAsync(id);

        if (!exists)
            return NotFound();

        await _categoriesService.DeleteAsync(id);

        return NoContent();
    }
}
