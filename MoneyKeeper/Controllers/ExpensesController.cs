using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Controllers.Abstractions;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.ExpenseFacades.Abstractions;
using MoneyKeeper.Validation.Abstractions;

namespace MoneyKeeper.Controllers;

[Route("api/expenses")]
public sealed class ExpensesController : BaseController
{
    private readonly IValidationService<NewExpenseDto> _validationService;
    private readonly IExpensesService _expensesService;

    public ExpensesController(IValidationService<NewExpenseDto> validationService, IExpensesService expensesService)
    {
        _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
        _expensesService = expensesService ?? throw new ArgumentNullException(nameof(expensesService));
    }

    [HttpGet]
    public Task<DataResult<ExpenseDto>> Get([FromQuery] ExpenseQueryCondition condition)
    {
        return _expensesService.GetAsync(condition);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseDto>> Get(Guid id)
    {
        ExpenseDto? result = await _expensesService.GetAsync(id);

        if (result is null)
            return NotFound();

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseDto>> Post(NewExpenseDto newExpenseDto)
    {
        IValidationResult validationResult = await _validationService.ValidateAsync(newExpenseDto);

        if (validationResult.IsFailed)
            return BadRequest(validationResult);

        return await _expensesService.CreateAsync(newExpenseDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ExpenseDto>> Put(Guid id, NewExpenseDto newExpenseDto)
    {
        bool exists = await _expensesService.ExistsAsync(id);

        if (!exists)
            return NotFound();

        IValidationResult validationResult = await _validationService.ValidateAsync(newExpenseDto);

        if (validationResult.IsFailed)
            return BadRequest(validationResult);

        return await _expensesService.UpdateAsync(id, newExpenseDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool exists = await _expensesService.ExistsAsync(id);

        if (!exists)
            return NotFound();

        await _expensesService.DeleteAsync(id);

        return NoContent();
    }
}
