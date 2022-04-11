using MoneyKeeper.Domain.AutoMapper;
using MoneyKeeper.Domain.Data.Models;
using MoneyKeeper.Domain.Data.Repositories;
using MoneyKeeper.Domain.Dtos;

namespace MoneyKeeper.Domain.Services;

public sealed class ExpenseService : IExpenseService
{
    private readonly IMapper _mapper;
    private readonly IExpenseRepository _expenseRepository;

    public ExpenseService(IMapper mapper, IExpenseRepository expenseRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
    }

    public async Task<ExpenseDto?> GetAsync(Guid id)
    {
        Expense? result = await _expenseRepository.GetAsync(id);
        
        return _mapper.Map<Expense, ExpenseDto>(result);
    }

    public async Task<DataResult<ExpenseDto>> GetAsync()
    {
        IEnumerable<Expense> result = await _expenseRepository.GetAsync();
        IEnumerable<ExpenseDto> resultDto = _mapper.Map<Expense, ExpenseDto>(result);

        return new DataResult<ExpenseDto>(resultDto);
    }

    public Task<bool> CreateAsync(NewExpenseDto expenseDto)
    {
        Expense expense = _mapper.Map<NewExpenseDto, Expense>(expenseDto)!;

        return _expenseRepository.CreateAsync(expense);
    }

    public Task<bool> UpdateAsync(Guid id, NewExpenseDto expenseDto)
    {
        Expense expense = _mapper.Map<NewExpenseDto, Expense>(expenseDto)!;

        return _expenseRepository.UpdateAsync(id, expense);
    }

    public Task DeleteAsync(Guid id)
    {
        return _expenseRepository.DeleteAsync(id);
    }
}
