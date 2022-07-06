using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.ExpenseFacades;
using MoneyKeeper.Infrastructure.Attributes;
using System.Net.Mime;

namespace MoneyKeeper.Controllers;

[ApiController, Route("api/expenses"), Authorize, Produces(MediaTypeNames.Application.Json)]
public sealed class ExpensesController : ControllerBase
{
    private readonly IExpenseQueriesService _queriesService;
    private readonly IExpenseCommandsService _commandsService;

    public ExpensesController(IExpenseQueriesService queriesService, IExpenseCommandsService commandsService)
    {
        _queriesService = queriesService ?? throw new ArgumentNullException(nameof(queriesService));
        _commandsService = commandsService ?? throw new ArgumentNullException(nameof(commandsService));
    }

    [HttpGet]
    public async Task<DataResult<ExpenseDto>> Get([FromQuery] ExpenseConditionDto expenseCondition)
    {
        return await _queriesService.GetAsync(expenseCondition);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseDto>> Get(Guid id)
    {
        ExpenseDto? result = await _queriesService.GetAsync(id);

        if (result is null)
            return NotFound();

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseDto>> Post(NewExpenseDto newExpenseDto)
    {
        Guid? resultId = await _commandsService.CreateAsync(newExpenseDto);

        if (!resultId.HasValue)
            return BadRequest();

        ExpenseDto result = (await _queriesService.GetAsync(resultId.Value))!;

        return result;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ExpenseDto>> Put(Guid id, NewExpenseDto newExpenseDto)
    {
        bool exists = await _queriesService.ExistsAsync(id);

        if (!exists)
            return NotFound();

        Guid? resultId = await _commandsService.UpdateAsync(id, newExpenseDto);

        if (!resultId.HasValue)
            return BadRequest();

        ExpenseDto result = (await _queriesService.GetAsync(resultId.Value))!;

        return result!;
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
