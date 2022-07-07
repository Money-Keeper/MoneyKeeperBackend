using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.UserFacades.Abstractions;

namespace MoneyKeeper.Facades.UserFacades;

internal sealed class UsersService : IUsersService
{
    private readonly IMapper _mapper;
    private readonly IUserQueries _userQueries;

    public UsersService(IMapper mapper, IUserQueries userQueries)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
    }

    public async Task<UserDto?> GetAsync(string login)
    {
        User? result = await _userQueries.GetAsync(login);
        UserDto? resultDto = _mapper.Map<User, UserDto>(result);

        // TODO: Need to refactor when nested list mapping will be implemented.

        if (resultDto is null)
            return null;

        resultDto.Wallets = _mapper.Map<Wallet, WalletDto>(result!.Wallets);
        return resultDto;
    }
}
