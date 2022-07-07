using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Infrastructure.Attributes;
using MoneyKeeper.Validation.Abstractions;
using System.Net.Mime;

namespace MoneyKeeper.Controllers.Abstractions;

[ApiController, Authorize, Produces(MediaTypeNames.Application.Json)]
public abstract class BaseController : ControllerBase
{
    protected virtual BadRequestObjectResult BadRequest(IValidationResult validationResult)
    {
        return new BadRequestObjectResult(validationResult);
    }
}
