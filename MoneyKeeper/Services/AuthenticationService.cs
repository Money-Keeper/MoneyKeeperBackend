using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Providers.Abstractions;
using MoneyKeeper.Domain.Queries.UserQueries;
using MoneyKeeper.Dtos;
using MoneyKeeper.Services.Abstractions;

namespace MoneyKeeper.Services;

internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly IQueryService<GetUserByLoginQuery, User?> _getUserByLoginService;
    private readonly IPasswordHashProvider _hashProvider;
    private readonly IJwtService _jwtService;

    public AuthenticationService(
        IQueryService<GetUserByLoginQuery, User?> getUserByLoginService,
        IPasswordHashProvider hashProvider,
        IJwtService jwtService)
    {
        _getUserByLoginService = getUserByLoginService ?? throw new ArgumentNullException(nameof(getUserByLoginService));
        _hashProvider = hashProvider ?? throw new ArgumentNullException(nameof(hashProvider));
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
    }

    public async Task<AuthenticationResponse?> AuthenticateAsync(AuthenticationRequest request)
    {
        User? user = await _getUserByLoginService.ExecuteAsync(new GetUserByLoginQuery(request.Login!));

        if (user is null)
            return null;

        bool verified = _hashProvider.Verify(request.Password!, user.PasswordHash);

        if (!verified)
            return null;

        string token = _jwtService.GetToken(user.Id);

        return new AuthenticationResponse
        {
            UserId = user.Id,
            Token = token
        };
    }
}
