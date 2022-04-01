using MoneyKeeper.Domain.Data.Models;
using MoneyKeeper.Domain.Data.Repositories;

namespace MoneyKeeper.Data.Repositories;

public class ExpenseMockRepository : IExpenseRepository
{
    private readonly List<Expense> _expenses;

    public ExpenseMockRepository()
    {
        _expenses = new List<Expense>
        {
            new Expense
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Amount = 12.99m,
                Date = DateTime.Now,
                Note = "Some note"
            },
            new Expense
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Amount = 34.99m,
                Date = DateTime.Now,
                Note = "Random note"
            },
            new Expense
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Amount = 56.99m,
                Date = DateTime.Now,
                Note = "Money waste"
            },
        };
    }

    public Task<Expense?> Get(Guid id)
    {
        Expense? result = _expenses.FirstOrDefault(x => x.Id == id);

        return Task.FromResult(result);
    }

    public Task<IEnumerable<Expense>> Get()
    {
        return Task.FromResult<IEnumerable<Expense>>(_expenses);
    }

    public Task<bool> CreateAsync(Expense expense)
    {
        expense.Id = Guid.NewGuid();
        expense.CreatedAt = DateTime.Now;

        _expenses.Add(expense);

        return Task.FromResult(true);
    }

    public Task<bool> UpdateAsync(Guid id, Expense expense)
    {
        int index = _expenses.FindIndex(x => x.Id == id);

        Expense existingExpense = _expenses[index];

        expense.Id = id;
        expense.CreatedAt = existingExpense.CreatedAt;
        expense.ModifiedAt = DateTime.Now;

        _expenses[index] = expense;

        return Task.FromResult(true);
    }

    public Task DeleteAsync(Guid id)
    {
        int index = _expenses.FindIndex(x => x.Id == id);

        _expenses.RemoveAt(index);

        return Task.CompletedTask;
    }
}
