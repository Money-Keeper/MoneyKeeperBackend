using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.UserQueries;
using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.UserFacades;

internal sealed class UserQueriesService : IUserQueriesService
{
    private readonly IMapper _mapper;
    private readonly IQueryService<GetUserByIdQuery, User?> _getUserByIdService;

    public UserQueriesService(
        IMapper mapper,
        IQueryService<GetUserByIdQuery, User?> getUserByIdService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _getUserByIdService = getUserByIdService ?? throw new ArgumentNullException(nameof(getUserByIdService));
    }

    public async Task<UserDto?> GetAsync(Guid id)
    {
        User? result = await _getUserByIdService.ExecuteAsync(new GetUserByIdQuery(id));

        if (result is null)
            return null;

        // TODO: Need to refactor.
        UserDto resultDto = _mapper.Map<User, UserDto>(result)!;
        resultDto.Wallets = _mapper.Map<Wallet, WalletDto>(result.Wallets);

        return resultDto;
    }
}
