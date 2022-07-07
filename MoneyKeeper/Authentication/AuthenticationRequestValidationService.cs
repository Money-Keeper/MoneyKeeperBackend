using MoneyKeeper.Authentication.Dtos;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Providers.Abstractions;
using MoneyKeeper.Domain.Queries.UserQueries;
using MoneyKeeper.Validation;
using MoneyKeeper.Validation.Abstractions;

namespace MoneyKeeper.Authentication;

internal sealed class AuthenticationRequestValidationService : IValidationService<AuthenticationRequest>
{
    private readonly IQueryService<GetUserByLoginQuery, User?> _getUserByLoginService;
    private readonly IPasswordHashProvider _hashProvider;

    public AuthenticationRequestValidationService(
        IQueryService<GetUserByLoginQuery, User?> getUserByLoginService,
        IPasswordHashProvider hashProvider)
    {
        _getUserByLoginService = getUserByLoginService ?? throw new ArgumentNullException(nameof(getUserByLoginService));
        _hashProvider = hashProvider ?? throw new ArgumentNullException(nameof(hashProvider));
    }

    public async Task<IValidationResult> ValidateAsync(AuthenticationRequest dto)
    {
        var result = new ValidationResult();

        await CheckUserPasswordAsync(dto, result);

        return result;
    }

    private async Task CheckUserPasswordAsync(AuthenticationRequest dto, ValidationResult result)
    {
        User? user = await _getUserByLoginService.ExecuteAsync(new GetUserByLoginQuery(dto.Login!));

        if (user is null)
        {
            result.AddError(nameof(dto.Login), "User does not exist");
            return;
        }

        bool verified = _hashProvider.Verify(dto.Password!, user.PasswordHash);

        if (!verified)
        {
            result.AddError(nameof(dto.Password), "Invalid password");
        }
    }
}
