using MoneyKeeper.Authentication.Dtos;

namespace MoneyKeeper.Authentication.Abstractions;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
}
