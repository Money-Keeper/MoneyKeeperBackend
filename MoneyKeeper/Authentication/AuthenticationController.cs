using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Authentication.Abstractions;
using MoneyKeeper.Authentication.Dtos;
using MoneyKeeper.Controllers.Abstractions;
using MoneyKeeper.Infrastructure.Attributes;
using MoneyKeeper.Validation.Abstractions;

namespace MoneyKeeper.Authentication;

[Route("api/auth")]
public sealed class AuthenticationController : BaseController
{
    private readonly IValidationService<AuthenticationRequest> _validationService;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(
        IValidationService<AuthenticationRequest> validationService,
        IAuthenticationService authenticationService)
    {
        _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
    }

    [HttpPost, AllowAnonymous]
    public async Task<ActionResult<AuthenticationResponse>> Post(AuthenticationRequest request)
    {
        IValidationResult validationResult = await _validationService.ValidateAsync(request);

        if (validationResult.IsFailed)
            return BadRequest(validationResult);

        return await _authenticationService.AuthenticateAsync(request);
    }
}
