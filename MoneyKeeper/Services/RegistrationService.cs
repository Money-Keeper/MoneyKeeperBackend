using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Commands.UserCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Providers.Abstractions;
using MoneyKeeper.Domain.Queries.UserQueries;
using MoneyKeeper.Dtos;
using MoneyKeeper.Services.Abstractions;

namespace MoneyKeeper.Services;

internal sealed class RegistrationService : IRegistrationService
{
    private readonly ICommandService<CreateUserCommand, CreateUserCommandResult> _createUserService;
    private readonly IQueryService<UserExistsQuery, bool> _userExistsByLoginService;
    private readonly IPasswordHashProvider _hashProvider;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public RegistrationService(
        ICommandService<CreateUserCommand, CreateUserCommandResult> createUserService,
        IQueryService<UserExistsQuery, bool> userExistsByLoginService,
        IPasswordHashProvider hashProvider,
        IJwtService jwtService,
        IMapper mapper)
    {
        _createUserService = createUserService ?? throw new ArgumentNullException(nameof(createUserService));
        _userExistsByLoginService = userExistsByLoginService ?? throw new ArgumentNullException(nameof(userExistsByLoginService));
        _hashProvider = hashProvider ?? throw new ArgumentNullException(nameof(hashProvider));
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<RegistrationResponse?> RegisterAsync(RegistrationRequest request)
    {
        bool exists = await _userExistsByLoginService.ExecuteAsync(new UserExistsQuery(request.Login!));

        if (exists)
            return null;

        User user = _mapper.Map<RegistrationRequest, User>(request)!;
        user.PasswordHash = _hashProvider.Hash(request.Password!);

        CreateUserCommandResult result = await _createUserService.ExecuteAsync(new CreateUserCommand(user));

        if (result.Data is null)
            return null;

        string token = _jwtService.GetToken(result.Data.Value);

        return new RegistrationResponse
        {
            UserId = result.Data.Value,
            Token = token
        };
    }
}
