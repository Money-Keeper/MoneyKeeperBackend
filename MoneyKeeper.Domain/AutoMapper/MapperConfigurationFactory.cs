using MoneyKeeper.Domain.Data.Models;
using MoneyKeeper.Domain.Dtos;

namespace MoneyKeeper.Domain.AutoMapper;

public class MapperConfigurationFactory
{
    public MapperConfiguration CreateMapperConfiguration()
    {
        return new MapperConfiguration(
            cfg => cfg.CreateMap<NewCurrencyDto, Currency>(),
            cfg => cfg.CreateMap<Currency, CurrencyDto>(),
            cfg => cfg.CreateMap<NewCategoryDto, Category>(),
            cfg => cfg.CreateMap<Category, CategoryDto>()
                .AddCustomMap(s => nameof(s.ParentCategory), t => nameof(t.ParentCategory)),
            cfg => cfg.CreateMap<NewExpenseDto, Expense>(),
            cfg => cfg.CreateMap<Expense, ExpenseDto>()
                .AddCustomMap(s => nameof(s.Currency), t => nameof(t.Currency))
                .AddCustomMap(s => nameof(s.Category), t => nameof(t.Category))
            );
    }
}
