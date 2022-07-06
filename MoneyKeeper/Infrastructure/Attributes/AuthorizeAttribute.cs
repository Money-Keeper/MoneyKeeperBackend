using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoneyKeeper.Infrastructure.UserContext;

namespace MoneyKeeper.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
internal sealed class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        bool allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymous)
            return;

        using IServiceScope scope = context.HttpContext.RequestServices.CreateScope();
        IUserContext userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();

        if (userContext.IsAuthorized)
            return;

        context.Result = new UnauthorizedObjectResult(null);
    }
}
