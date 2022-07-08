using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.UserFacades.Abstractions;
using MoneyKeeper.Infrastructure.UserContext;

namespace MoneyKeeper.Facades.UserFacades;

internal sealed class UsersService : IUsersService
{
    private readonly IMapper _mapper;
    private readonly IUserQueries _userQueries;
    private readonly IUserContext _userContext;

    public UsersService(IMapper mapper, IUserQueries userQueries, IUserContext userContext)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
    }

    public async Task<UserDto?> GetAsync()
    {
        User? result = await _userQueries.GetAsync(_userContext.CurrentUserId!.Value);
        return _mapper.Map<User, UserDto>(result);
    }
}
