using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Queries.UserQueries;
using MoneyKeeper.Registration.Dtos;
using MoneyKeeper.Validation;
using MoneyKeeper.Validation.Abstractions;

namespace MoneyKeeper.Registration;

internal sealed class RegistrationRequestValidationService : IValidationService<RegistrationRequest>
{
    private readonly IQueryService<UserExistsByLoginQuery, bool> _userExistsByLoginService;

    public RegistrationRequestValidationService(IQueryService<UserExistsByLoginQuery, bool> userExistsByLoginService)
    {
        _userExistsByLoginService = userExistsByLoginService ?? throw new ArgumentNullException(nameof(userExistsByLoginService));
    }

    public async Task<IValidationResult> ValidateAsync(RegistrationRequest dto)
    {
        var result = new ValidationResult();

        await CheckUserAsync(dto, result);

        return result;
    }

    private async Task CheckUserAsync(RegistrationRequest dto, ValidationResult result)
    {
        bool userExists = await _userExistsByLoginService.ExecuteAsync(new UserExistsByLoginQuery(dto.Login!));

        if (userExists)
        {
            result.AddError(nameof(dto.Login), "Such user already exist");
        }
    }
}
