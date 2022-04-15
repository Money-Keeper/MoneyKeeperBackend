﻿using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Domain.Dtos;
using MoneyKeeper.Domain.Services.Abstractions;

namespace MoneyKeeper.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
    }

    [HttpGet]
    public async Task<DataResult<CategoryDto>> Get() => await _categoryService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> Get(Guid id)
    {
        CategoryDto? category = await _categoryService.GetAsync(id);

        if (category is null)
            return NotFound();

        return category;
    }

    [HttpPost]
    public async Task<ActionResult> Post(NewCategoryDto newCategoryDto)
    {
        bool result = await _categoryService.CreateAsync(newCategoryDto);

        return result ? Ok() : BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, NewCategoryDto newCategoryDto)
    {
        bool isExists = await _categoryService.IsExistsAsync(id);

        if (!isExists)
            return NotFound();

        bool result = await _categoryService.UpdateAsync(id, newCategoryDto);

        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        bool isExists = await _categoryService.IsExistsAsync(id);

        if (!isExists)
            return NotFound();

        await _categoryService.DeleteAsync(id);

        return NoContent();
    }
}