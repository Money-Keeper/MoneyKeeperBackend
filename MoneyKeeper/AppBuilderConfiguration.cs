using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Data;
using MoneyKeeper.Data.Repositories;
using MoneyKeeper.Domain.Data.Repositories;
using MoneyKeeper.Domain.Mappers;
using MoneyKeeper.Domain.Providers;
using MoneyKeeper.Domain.Services;

namespace MoneyKeeper;

internal static class AppBuilderConfiguration
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services
            .AddDbContext<MoneyKeeperContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString")))
            .AddSingleton<IMapperConfiguration, MapperConfiguration>(_ => new MapperConfigurationFactory().CreateMapperConfiguration())
            .AddSingleton<IMapper, Mapper>()
            .AddTransient<IDateTimeProvider, DateTimeProvider>()
            .AddScoped<IExpenseRepository, ExpenseRepository>()
            .AddScoped<IExpenseService, ExpenseService>()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return builder;
    }
}
