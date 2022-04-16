namespace MoneyKeeper.Middleware;

internal interface IExceptionHandler
{
    Task Handle(HttpContext context);
}
