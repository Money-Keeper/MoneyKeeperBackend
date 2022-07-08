using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.ExpenseFacades.Abstractions;

public interface IExpensesService
{
    Task<ExpenseDto> CreateAsync(NewExpenseDto dto);
    Task<ExpenseDto> UpdateAsync(Guid id, NewExpenseDto dto);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<ExpenseDto?> GetAsync(Guid id);
    Task<DataResult<ExpenseDto>> GetAsync(ExpenseQueryCondition condition);
}
