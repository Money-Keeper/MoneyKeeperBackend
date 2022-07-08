using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.WalletFacades.Abstractions;
using MoneyKeeper.Infrastructure.UserContext;

namespace MoneyKeeper.Facades.WalletFacades;

internal sealed class WalletsService : IWalletsService
{
    private readonly IMapper _mapper;
    private readonly IWalletCommands _walletCommands;
    private readonly IWalletQueries _walletQueries;
    private readonly IUserContext _userContext;

    public WalletsService(IMapper mapper, IWalletCommands walletCommands, IWalletQueries walletQueries, IUserContext userContext)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _walletCommands = walletCommands ?? throw new ArgumentNullException(nameof(walletCommands));
        _walletQueries = walletQueries ?? throw new ArgumentNullException(nameof(walletQueries));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
    }

    public async Task<WalletDto> CreateAsync(NewWalletDto dto)
    {
        Wallet result = await _walletCommands.CreateAsync(
            _mapper.Map<NewWalletDto, Wallet>(dto)!, _userContext.CurrentUserId!.Value);

        return _mapper.Map<Wallet, WalletDto>(result)!;
    }

    public Task DeleteAsync(Guid id) => _walletCommands.DeleteAsync(id);

    public Task<bool> ExistsAsync(Guid id) => _walletQueries.ExistsAsync(id);

    public async Task<WalletDto?> GetAsync(Guid id)
    {
        Wallet? result = await _walletQueries.GetAsync(id, _userContext.CurrentUserId!.Value);
        return _mapper.Map<Wallet, WalletDto>(result);
    }

    public async Task<DataResult<WalletDto>> GetAsync()
    {
        IEnumerable<Wallet> result = await _walletQueries.GetAsync(_userContext.CurrentUserId!.Value);
        return new DataResult<WalletDto>(_mapper.Map<Wallet, WalletDto>(result));
    }

    public async Task<WalletDto> UpdateAsync(Guid id, NewWalletDto dto)
    {
        Wallet result = await _walletCommands.UpdateAsync(id, _mapper.Map<NewWalletDto, Wallet>(dto)!);
        return _mapper.Map<Wallet, WalletDto>(result)!;
    }
}
