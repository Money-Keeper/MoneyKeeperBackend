using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MoneyKeeper.AutoMapper;
using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Data;
using MoneyKeeper.Data.QueryServices;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Infrastructure.Events;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Providers;
using MoneyKeeper.Domain.Providers.Abstractions;
using MoneyKeeper.Domain.Queries;
using MoneyKeeper.Domain.Tools;
using MoneyKeeper.Domain.Tools.Abstractions;
using MoneyKeeper.Factories;
using MoneyKeeper.Infrastructure.Settings;
using MoneyKeeper.Infrastructure.UserContext;
using MoneyKeeper.Services;
using MoneyKeeper.Services.Abstractions;
using System.Reflection;
using System.Text.Json;

namespace MoneyKeeper;

internal static class AppBuilderConfiguration
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

        builder.Services
            .AddHttpContextAccessor()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options => options.SchemaGeneratorOptions.SupportNonNullableReferenceTypes = true)
            .AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var settings = serviceProvider.GetRequiredService<ISettingsService>().GetSettings<DbSettings>();
                options.UseNpgsql(settings.ConnectionString);
            });

        builder.Services
            .AddSingleton<IUserContext, UserContext>()
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            .AddSingleton<IPathConverter, PathConverter>()
            .AddSingleton<IFileNameProvider, FileNameProvider>()
            .AddSingleton<IPasswordHashProvider, PasswordHashProvider>()
            .AddSingleton<ISettingsService, SettingsService>()
            .AddSingleton<IJwtService, JwtService>()
            .AddSingleton<TokenValidationParametersFactory>()
            .AddSingleton<IFileDirectoryProvider>(serviceProvider =>
            {
                var settingsService = serviceProvider.GetRequiredService<ISettingsService>();
                return new FileDirectoryProviderFactory().Create(settingsService);
            })
            .AddScoped<IQueryService<EntityExistsQuery<Currency>, bool>, EntityExistsQueryService<Currency>>()
            .AddScoped<IQueryService<EntityExistsQuery<Category>, bool>, EntityExistsQueryService<Category>>()
            .AddScoped<IQueryService<EntityExistsQuery<Expense>, bool>, EntityExistsQueryService<Expense>>();

        builder.Services
            .AddAutoMapper()
            .AddServices()
            .AddFacades();

        return builder;
    }

    private static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        return services
            .AddSingleton<IMapperConfiguration>(_ => new MapperConfigurationFactory().Create())
            .AddSingleton<IMapper, Mapper>();
    }

    private static IServiceCollection AddFacades(this IServiceCollection services)
    {
        foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
        {
            bool validTypeName = type.Name.EndsWith("Commands") || type.Name.EndsWith("Queries");

            if (!type.IsAbstract && validTypeName)
            {
                services.TryAddScoped(type.GetInterfaces().Single(), type);
            }
        }

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        Assembly webApiAssembly = Assembly.GetExecutingAssembly();
        Assembly dataAssembly = typeof(AppDbContext).Assembly;
        Assembly domainAssembly = typeof(IQuery<>).Assembly;

        foreach (Type type in webApiAssembly.GetTypes())
        {
            if (!type.IsAbstract && type.Name.EndsWith("Service"))
            {
                services.TryAddScoped(type.GetInterfaces().Single(), type);
            }
        }

        services
            .AddAssemblyGenericTypesOf(typeof(ICommandService<,>), ServiceLifetime.Scoped, dataAssembly, domainAssembly)
            .AddAssemblyGenericTypesOf(typeof(IQueryService<,>), ServiceLifetime.Scoped, dataAssembly, domainAssembly)
            .AddAssemblyGenericTypesOf(typeof(IAsyncEventHandler<>), ServiceLifetime.Scoped, domainAssembly);

        return services;
    }

    private static IServiceCollection AddAssemblyGenericTypesOf(
        this IServiceCollection services,
        Type abstraction,
        ServiceLifetime lifetime,
        params Assembly[] assemblies)
    {
        List<Type> types = new List<Type>();

        foreach (Assembly assembly in assemblies)
        {
            types.AddRange(assembly.GetTypes());
        }

        var mappings = from type in types
                       where !type.IsAbstract && !type.IsGenericType
                       from i in type.GetInterfaces()
                       where i.IsGenericType
                       let gType = i.GetGenericTypeDefinition()
                       where gType == abstraction
                       select (i, type);

        foreach ((Type service, Type implementation) in mappings)
        {
            services.TryAdd(new ServiceDescriptor(service, implementation, lifetime));
        }

        return services;
    }
}
