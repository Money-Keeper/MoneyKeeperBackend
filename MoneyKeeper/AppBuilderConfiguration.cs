using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Data;
using MoneyKeeper.Data.Repositories;
using MoneyKeeper.Domain.AutoMapper;
using MoneyKeeper.Domain.Data.Repositories;
using MoneyKeeper.Domain.Providers;
using MoneyKeeper.Domain.Services;

namespace MoneyKeeper;

internal static class AppBuilderConfiguration
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options => options.SchemaGeneratorOptions.SupportNonNullableReferenceTypes = true);

        IMapperConfiguration mapperConfiguration = new MapperConfigurationFactory().CreateMapperConfiguration();

        builder.Services
            .AddDbContext<MoneyKeeperContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString")))
            .AddSingleton(mapperConfiguration)
            .AddSingleton<IMapper, Mapper>()
            .AddScoped<IDateTimeProvider, DateTimeProvider>()
            .AddScoped<ICurrencyRepository, CurrencyRepository>()
            .AddScoped<ICurrencyService, CurrencyService>()
            .AddScoped<IExpenseRepository, ExpenseRepository>()
            .AddScoped<IExpenseService, ExpenseService>();

        return builder;
    }
}
