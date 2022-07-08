using MoneyKeeper.Authentication.Abstractions;
using MoneyKeeper.Authentication.Dtos;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.UserQueries;
using MoneyKeeper.Services.Abstractions;

namespace MoneyKeeper.Authentication;

internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly IQueryService<GetUserByLoginQuery, User?> _getUserByLoginService;
    private readonly IJwtService _jwtService;

    public AuthenticationService(IQueryService<GetUserByLoginQuery, User?> getUserByLoginService, IJwtService jwtService)
    {
        _getUserByLoginService = getUserByLoginService ?? throw new ArgumentNullException(nameof(getUserByLoginService));
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
    }

    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
    {
        User user = (await _getUserByLoginService.ExecuteAsync(new GetUserByLoginQuery(request.Login!)))!;

        string token = _jwtService.GetToken(user.Id);

        return new AuthenticationResponse
        {
            UserId = user.Id,
            Token = token
        };
    }
}
