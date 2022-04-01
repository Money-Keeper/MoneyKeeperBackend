using MoneyKeeper.Domain.Data.Models;
using MoneyKeeper.Domain.Dtos;

namespace MoneyKeeper.Domain.Mappers;

public class MapperConfigurationFactory
{
    public MapperConfiguration CreateMapperConfiguration()
    {
        MapperConfiguration configuration = new MapperConfiguration();

        configuration.CreateMapper<Expense, ExpenseDto>();
        configuration.CreateMapper<NewExpenseDto, Expense>();

        return configuration;
    }
}
