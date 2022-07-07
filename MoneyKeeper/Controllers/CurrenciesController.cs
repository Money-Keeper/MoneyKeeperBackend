using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Controllers.Abstractions;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.CurrencyFacades.Abstractions;

namespace MoneyKeeper.Controllers;

[Route("api/currencies")]
public sealed class CurrenciesController : BaseController
{
    private readonly ICurrenciesService _currenciesService;

    public CurrenciesController(ICurrenciesService currenciesService)
    {
        _currenciesService = currenciesService ?? throw new ArgumentNullException(nameof(currenciesService));
    }

    [HttpGet]
    public Task<DataResult<CurrencyDto>> Get() => _currenciesService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<CurrencyDto>> Get(Guid id)
    {
        CurrencyDto? result = await _currenciesService.GetAsync(id);

        if (result is null)
            return NotFound();

        return result;
    }

    [HttpPost]
    public Task<CurrencyDto> Post(NewCurrencyDto newCurrencyDto)
    {
        return _currenciesService.CreateAsync(newCurrencyDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CurrencyDto>> Put(Guid id, NewCurrencyDto newCurrencyDto)
    {
        bool exists = await _currenciesService.ExistsAsync(id);

        if (!exists)
            return NotFound();

        return await _currenciesService.UpdateAsync(id, newCurrencyDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool exists = await _currenciesService.ExistsAsync(id);

        if (!exists)
            return NotFound();

        await _currenciesService.DeleteAsync(id);

        return NoContent();
    }
}
