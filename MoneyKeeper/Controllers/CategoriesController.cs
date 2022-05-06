using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.CategoryFacades;
using System.Globalization;
using System.Net.Mime;

namespace MoneyKeeper.Controllers;

[ApiController]
[Route("api/categories")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class CategoriesController : ControllerBase
{
    private readonly ICategoryQueriesService _queriesService;
    private readonly ICategoryCommandsService _commandsService;

    public CategoriesController(ICategoryQueriesService queriesService, ICategoryCommandsService commandsService)
    {
        _queriesService = queriesService ?? throw new ArgumentNullException(nameof(queriesService));
        _commandsService = commandsService ?? throw new ArgumentNullException(nameof(commandsService));
    }

    [HttpGet]
    public async Task<DataResult<CategoryDto>> Get() => await _queriesService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> Get(Guid id)
    {
        CategoryDto? result = await _queriesService.GetAsync(id);

        if (result is null)
            return NotFound();

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Post(NewCategoryDto newCategoryDto)
    {
        if (!IsHex(newCategoryDto.Color!))
            return BadRequest();

        CategoryDto? result = await _commandsService.CreateAsync(newCategoryDto);

        if (result is null)
            return BadRequest();

        return result;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDto>> Put(Guid id, NewCategoryDto newCategoryDto)
    {
        bool exists = await _queriesService.ExistsAsync(id);

        if (!exists)
            return NotFound();

        if (!IsHex(newCategoryDto.Color!))
            return BadRequest();

        CategoryDto? result = await _commandsService.UpdateAsync(id, newCategoryDto);

        if (result is null)
            return BadRequest();

        return result;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool exists = await _queriesService.ExistsAsync(id);

        if (!exists)
            return NotFound();

        await _commandsService.DeleteAsync(id);

        return NoContent();
    }

    private bool IsHex(string str)
    {
        return int.TryParse(str, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out _);
    }
}
