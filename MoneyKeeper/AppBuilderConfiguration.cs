using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Data;
using MoneyKeeper.Data.Repositories;
using MoneyKeeper.Domain.AutoMapper;
using MoneyKeeper.Domain.Data.Abstractions;
using MoneyKeeper.Domain.Data.Abstractions.Repositories;
using MoneyKeeper.Domain.Providers;
using MoneyKeeper.Domain.Providers.Abstractions;
using MoneyKeeper.Domain.Providers.FilesProvider;
using MoneyKeeper.Domain.Services;
using MoneyKeeper.Domain.Services.Abstractions;
using MoneyKeeper.Domain.Tools;
using MoneyKeeper.Domain.Tools.Abstractions;

namespace MoneyKeeper;

internal static class AppBuilderConfiguration
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services
            .AddDbContext<MoneyKeeperContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString")));

        AddSwaggerGen();
        AddMapper();

        builder.AddFilesProvider();

        builder.Services
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            .AddSingleton<IPathConverter, PathConverter>()
            .AddScoped<IEntityHelper, EntityHelper>()
            .AddScoped<ICurrencyRepository, CurrencyRepository>()
            .AddScoped<ICurrencyService, CurrencyService>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<ICategoryService, CategoryService>()
            .AddScoped<IExpenseRepository, ExpenseRepository>()
            .AddScoped<IExpenseService, ExpenseService>();

        return builder;

        void AddSwaggerGen()
        {
            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(options => options.SchemaGeneratorOptions.SupportNonNullableReferenceTypes = true);
        }

        void AddMapper()
        {
            IMapperConfiguration mapperConfiguration = new MapperConfigurationFactory().CreateMapperConfiguration();

            builder.Services
                .AddSingleton(mapperConfiguration)
                .AddSingleton<IMapper, Mapper>();
        }
    }

    private static WebApplicationBuilder AddFilesProvider(this WebApplicationBuilder builder)
    {
        const string FolderNames = nameof(FolderNames);
        const string ImagesFolder = nameof(ImagesFolder);
        const string PdfFolder = nameof(PdfFolder);

        IConfigurationSection section = builder.Configuration.GetRequiredSection(FolderNames);
        string imagesRootDirectory = Path.Combine(AppContext.BaseDirectory, section.GetRequiredSection(ImagesFolder).Value);
        string pdfRootDirectory = Path.Combine(AppContext.BaseDirectory, section.GetRequiredSection(PdfFolder).Value);

        builder.Services
            .AddSingleton<FilesProviderBase, ImagesProvider>(_ => new ImagesProvider(imagesRootDirectory))
            .AddSingleton<FilesProviderBase, PdfProvider>(_ => new PdfProvider(pdfRootDirectory))
            .AddSingleton<IFilesProvider, FilesProvider>();

        return builder;
    }
}
