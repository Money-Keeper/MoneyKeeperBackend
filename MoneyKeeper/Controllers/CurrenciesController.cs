using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.CurrencyFacades;
using System.Net.Mime;

namespace MoneyKeeper.Controllers;

[ApiController]
[Route("api/currencies")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class CurrenciesController : ControllerBase
{
    private readonly ICurrencyQueriesService _queriesService;
    private readonly ICurrencyCommandsService _commandsService;

    public CurrenciesController(ICurrencyQueriesService queriesService, ICurrencyCommandsService commandsService)
    {
        _queriesService = queriesService ?? throw new ArgumentNullException(nameof(queriesService));
        _commandsService = commandsService ?? throw new ArgumentNullException(nameof(commandsService));
    }

    [HttpGet]
    public async Task<DataResult<CurrencyDto>> Get() => await _queriesService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<CurrencyDto>> Get(Guid id)
    {
        CurrencyDto? result = await _queriesService.GetAsync(id);

        if (result is null)
            return NotFound();

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<CurrencyDto>> Post(NewCurrencyDto newCurrencyDto)
    {
        CurrencyDto? result = await _commandsService.CreateAsync(newCurrencyDto);

        if (result is null)
            return BadRequest();

        return result;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CurrencyDto>> Put(Guid id, NewCurrencyDto newCurrencyDto)
    {
        bool exists = await _queriesService.ExistsAsync(id);

        if (!exists)
            return NotFound();

        CurrencyDto? result = await _commandsService.UpdateAsync(id, newCurrencyDto);

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
}
