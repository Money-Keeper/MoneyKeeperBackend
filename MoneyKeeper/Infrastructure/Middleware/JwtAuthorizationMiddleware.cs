using MoneyKeeper.Services.Abstractions;

namespace MoneyKeeper.Infrastructure.Middleware;

internal sealed class JwtAuthorizationMiddleware
{
    private const string UserId = nameof(UserId);
    private const string AuthorizationHeader = "Authorization";
    private const int TokenPrefixLength = 7;

    private readonly RequestDelegate _next;
    private readonly IJwtService _jwtService;

    public JwtAuthorizationMiddleware(RequestDelegate next, IJwtService jwtService)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? token = context.Request.Headers[AuthorizationHeader].FirstOrDefault()?.Substring(TokenPrefixLength);

        if (token != null && _jwtService.ValidateToken(token, out Guid? userId))
        {
            context.Items[UserId] = userId;
        }

        await _next(context);
    }
}
