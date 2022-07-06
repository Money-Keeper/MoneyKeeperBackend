namespace MoneyKeeper.Infrastructure.Middleware;

internal sealed class ExceptionHandler : IExceptionHandler
{
    public Task HandleAsync(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return Task.CompletedTask;
    }
}
