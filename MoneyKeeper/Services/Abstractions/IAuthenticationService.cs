using MoneyKeeper.Dtos;

namespace MoneyKeeper.Services.Abstractions;

public interface IAuthenticationService
{
    Task<AuthenticationResponse?> AuthenticateAsync(AuthenticationRequest request);
}
