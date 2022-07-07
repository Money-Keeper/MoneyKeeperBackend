using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Controllers.Abstractions;
using MoneyKeeper.Infrastructure.Attributes;
using MoneyKeeper.Registration.Abstractions;
using MoneyKeeper.Registration.Dtos;
using MoneyKeeper.Validation.Abstractions;

namespace MoneyKeeper.Registration;

[Route("api/register")]
public sealed class RegistrationController : BaseController
{
    private readonly IValidationService<RegistrationRequest> _validationService;
    private readonly IRegistrationService _registrationService;

    public RegistrationController(
        IValidationService<RegistrationRequest> validationService,
        IRegistrationService registrationService)
    {
        _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
        _registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
    }

    [HttpPost, AllowAnonymous]
    public async Task<ActionResult<RegistrationResponse>> Post(RegistrationRequest dto)
    {
        IValidationResult validationResult = await _validationService.ValidateAsync(dto);

        if (validationResult.IsFailed)
            return BadRequest(validationResult);

        return await _registrationService.RegisterAsync(dto);
    }
}
