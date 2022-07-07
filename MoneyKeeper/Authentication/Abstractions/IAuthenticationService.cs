using MoneyKeeper.Authentication.Dtos;

namespace MoneyKeeper.Authentication.Abstractions;

public interface IAuthenticationService
{
    AuthenticationResponse Authenticate(AuthenticationRequest request);
}
