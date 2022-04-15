using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Data;
using MoneyKeeper.Data.Repositories;
using MoneyKeeper.Domain.AutoMapper;
using MoneyKeeper.Domain.Data.Abstractions;
using MoneyKeeper.Domain.Data.Abstractions.Repositories;
using MoneyKeeper.Domain.Providers;
using MoneyKeeper.Domain.Services;
using MoneyKeeper.Domain.Services.Abstractions;
using MoneyKeeper.Domain.Tools;

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
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            .AddScoped<IEntityHelper, EntityHelper>()
            .AddScoped<ICurrencyRepository, CurrencyRepository>()
            .AddScoped<ICurrencyService, CurrencyService>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<ICategoryService, CategoryService>()
            .AddScoped<IExpenseRepository, ExpenseRepository>()
            .AddScoped<IExpenseService, ExpenseService>();

        return builder;
    }
}
