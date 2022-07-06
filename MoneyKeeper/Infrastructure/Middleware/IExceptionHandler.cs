namespace MoneyKeeper.Infrastructure.Middleware;

internal interface IExceptionHandler
{
    Task HandleAsync(HttpContext context);
}
