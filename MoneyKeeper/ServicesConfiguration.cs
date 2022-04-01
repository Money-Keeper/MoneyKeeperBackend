using MoneyKeeper.Data.Repositories;
using MoneyKeeper.Domain.Data.Repositories;
using MoneyKeeper.Domain.Mappers;
using MoneyKeeper.Domain.Services;

namespace MoneyKeeper;

internal static class ServicesConfiguration
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IMapperConfiguration, MapperConfiguration>(_ =>
        {
            return new MapperConfigurationFactory().CreateMapperConfiguration();
        });
        builder.Services.AddSingleton<IMapper, Mapper>();
        builder.Services.AddSingleton<IExpenseRepository, ExpenseMockRepository>();
        builder.Services.AddScoped<IExpenseService, ExpenseService>();
    }
}
