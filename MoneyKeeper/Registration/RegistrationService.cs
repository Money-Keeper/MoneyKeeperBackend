using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Commands.UserCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Providers.Abstractions;
using MoneyKeeper.Registration.Abstractions;
using MoneyKeeper.Registration.Dtos;
using MoneyKeeper.Services.Abstractions;

namespace MoneyKeeper.Registration;

internal sealed class RegistrationService : IRegistrationService
{
    private readonly IMapper _mapper;
    private readonly ICommandService<CreateUserCommand, CreateUserCommandResult> _createUserService;
    private readonly IPasswordHashProvider _hashProvider;
    private readonly IJwtService _jwtService;

    public RegistrationService(
        IMapper mapper,
        ICommandService<CreateUserCommand, CreateUserCommandResult> createUserService,
        IPasswordHashProvider hashProvider,
        IJwtService jwtService)
    {
        _createUserService = createUserService ?? throw new ArgumentNullException(nameof(createUserService));
        _hashProvider = hashProvider ?? throw new ArgumentNullException(nameof(hashProvider));
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
    {
        User user = _mapper.Map<RegistrationRequest, User>(request)!;
        user.PasswordHash = _hashProvider.Hash(request.Password!);

        Guid userId = (await _createUserService.ExecuteAsync(new CreateUserCommand(user))).Data.Id;

        string token = _jwtService.GetToken(userId);

        return new RegistrationResponse
        {
            UserId = userId,
            Token = token
        };
    }
}
