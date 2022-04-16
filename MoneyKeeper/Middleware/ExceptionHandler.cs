namespace MoneyKeeper.Middleware;

internal sealed class ExceptionHandler : IExceptionHandler
{
    public Task Handle(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return Task.CompletedTask;
    }
}
