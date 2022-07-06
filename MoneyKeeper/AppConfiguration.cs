using Microsoft.EntityFrameworkCore;
using MoneyKeeper.Data;
using MoneyKeeper.Infrastructure.Middleware;

namespace MoneyKeeper;

internal static class AppConfiguration
{
    private const string DbMigrationCommand = "-m";

    public static WebApplication ConfigureApp(this WebApplication app, string[] args)
    {
        if (args.Length > 0 && args[0] == DbMigrationCommand)
        {
            using IServiceScope scope = app.Services.CreateScope();
            scope.ServiceProvider.GetService<AppDbContext>()!.Database.Migrate();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        IExceptionHandler exceptionHandler = new ExceptionHandler();

        app.UseMiddleware<JwtAuthorizationMiddleware>();
        app.UseExceptionHandler(a => a.Run(exceptionHandler.HandleAsync));
        app.MapControllers();

        return app;
    }
}
