using MoneyKeeper.Domain.Dtos;

namespace MoneyKeeper.Domain.Services.Abstractions;

public interface IExpenseService
{
    Task<bool> ExistsAsync(Guid id);
    Task<ExpenseDto?> GetAsync(Guid id);
    Task<DataResult<ExpenseDto>> GetAsync();
    Task<bool> CreateAsync(NewExpenseDto expenseDto);
    Task<bool> UpdateAsync(Guid id, NewExpenseDto expenseDto);
    Task DeleteAsync(Guid id);
}
