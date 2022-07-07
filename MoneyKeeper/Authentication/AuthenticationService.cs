using MoneyKeeper.Authentication.Abstractions;
using MoneyKeeper.Authentication.Dtos;
using MoneyKeeper.Services.Abstractions;

namespace MoneyKeeper.Authentication;

internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly IJwtService _jwtService;

    public AuthenticationService(IJwtService jwtService)
    {
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
    }

    public AuthenticationResponse Authenticate(AuthenticationRequest request)
    {
        string token = _jwtService.GetToken(request.Login!);

        return new AuthenticationResponse
        {
            Login = request.Login!,
            Token = token
        };
    }
}
