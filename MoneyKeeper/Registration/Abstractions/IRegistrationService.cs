using MoneyKeeper.Registration.Dtos;

namespace MoneyKeeper.Registration.Abstractions;

public interface IRegistrationService
{
    Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
}
