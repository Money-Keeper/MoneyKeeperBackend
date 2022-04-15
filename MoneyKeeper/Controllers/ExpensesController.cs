﻿using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Domain.Dtos;
using MoneyKeeper.Domain.Services.Abstractions;

namespace MoneyKeeper.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpensesController(IExpenseService expenseService)
    {
        _expenseService = expenseService ?? throw new ArgumentNullException(nameof(expenseService));
    }

    [HttpGet]
    public async Task<DataResult<ExpenseDto>> Get() => await _expenseService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseDto>> Get(Guid id)
    {
        ExpenseDto? expense = await _expenseService.GetAsync(id);

        if (expense is null)
            return NotFound();

        return expense;
    }

    [HttpPost]
    public async Task<ActionResult> Post(NewExpenseDto newExpenseDto)
    {
        bool result = await _expenseService.CreateAsync(newExpenseDto);

        return result ? Ok() : BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, NewExpenseDto newExpenseDto)
    {
        bool isExists = await _expenseService.IsExistsAsync(id);

        if (!isExists)
            return NotFound();

        bool result = await _expenseService.UpdateAsync(id, newExpenseDto);

        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        bool isExists = await _expenseService.IsExistsAsync(id);

        if (!isExists)
            return NotFound();

        await _expenseService.DeleteAsync(id);

        return NoContent();
    }
}
