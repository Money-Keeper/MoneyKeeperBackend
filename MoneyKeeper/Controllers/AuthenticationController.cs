using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Dtos;
using MoneyKeeper.Services.Abstractions;
using System.Net.Mime;

namespace MoneyKeeper.Controllers;

[ApiController, Route("api/auth"), Produces(MediaTypeNames.Application.Json)]
public sealed class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
    }

    [HttpPost]
    public async Task<ActionResult<AuthenticationResponse>> Post(AuthenticationRequest request)
    {
        AuthenticationResponse? result = await _authenticationService.AuthenticateAsync(request);

        if (result is null)
            return BadRequest();

        return result;
    }
}
