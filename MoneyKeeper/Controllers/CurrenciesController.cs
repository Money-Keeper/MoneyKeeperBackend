﻿using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Domain.Dtos;
using MoneyKeeper.Domain.Services.Abstractions;

namespace MoneyKeeper.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class CurrenciesController : ControllerBase
{
    private readonly ICurrencyService _currencyService;

    public CurrenciesController(ICurrencyService currencyService)
    {
        _currencyService = currencyService ?? throw new ArgumentNullException(nameof(currencyService));
    }

    [HttpGet]
    public async Task<DataResult<CurrencyDto>> Get() => await _currencyService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<CurrencyDto>> Get(Guid id)
    {
        CurrencyDto? currency = await _currencyService.GetAsync(id);

        if (currency is null)
            return NotFound();

        return currency;
    }

    [HttpPost]
    public async Task<ActionResult> Post(NewCurrencyDto newCurrencyDto)
    {
        bool result = await _currencyService.CreateAsync(newCurrencyDto);

        return result ? Ok() : BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, NewCurrencyDto newCurrencyDto)
    {
        bool isExists = await _currencyService.IsExistsAsync(id);

        if (!isExists)
            return NotFound();

        bool result = await _currencyService.UpdateAsync(id, newCurrencyDto);

        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        bool isExists = await _currencyService.IsExistsAsync(id);

        if (!isExists)
            return NotFound();

        await _currencyService.DeleteAsync(id);

        return NoContent();
    }
}
