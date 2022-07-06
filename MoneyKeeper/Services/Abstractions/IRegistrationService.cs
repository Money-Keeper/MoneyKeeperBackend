using MoneyKeeper.Dtos;

namespace MoneyKeeper.Services.Abstractions;

public interface IRegistrationService
{
    Task<RegistrationResponse?> RegisterAsync(RegistrationRequest request);
}
