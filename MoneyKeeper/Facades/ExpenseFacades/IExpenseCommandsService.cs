using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.ExpenseFacades;

public interface IExpenseCommandsService
{
    Task<Guid?> CreateAsync(NewExpenseDto newExpense);
    Task<Guid?> UpdateAsync(Guid id, NewExpenseDto newExpense);
    Task DeleteAsync(Guid id);
}
