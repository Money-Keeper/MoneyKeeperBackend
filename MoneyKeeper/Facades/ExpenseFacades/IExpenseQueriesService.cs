using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.ExpenseFacades;

public interface IExpenseQueriesService
{
    Task<bool> ExistsAsync(Guid id);
    Task<ExpenseDto?> GetAsync(Guid id);
    Task<DataResult<ExpenseDto>> GetAsync(ExpenseConditionDto condition);
}
