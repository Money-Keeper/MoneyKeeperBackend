using MoneyKeeper.Domain.Data.Models;
using MoneyKeeper.Domain.Dtos;

namespace MoneyKeeper.Domain.Services;

public interface IExpenseService
{
    Task<ExpenseDto?> GetAsync(Guid id);
    Task<DataResult<ExpenseDto>> GetAsync();
    Task<bool> CreateAsync(NewExpenseDto expenseDto);
    Task<bool> UpdateAsync(Guid id, NewExpenseDto expenseDto);
    Task DeleteAsync(Guid id);
}
