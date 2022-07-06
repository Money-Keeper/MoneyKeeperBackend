using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Dtos;
using MoneyKeeper.Services.Abstractions;
using System.Net.Mime;

namespace MoneyKeeper.Controllers;

[ApiController, Route("api/register"), Produces(MediaTypeNames.Application.Json)]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationController(IRegistrationService registrationService)
    {
        _registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
    }

    [HttpPost]
    public async Task<ActionResult<RegistrationResponse>> Post(RegistrationRequest dto)
    {
        RegistrationResponse? result = await _registrationService.RegisterAsync(dto);

        if (result is null)
            return BadRequest();

        return result;
    }
}
