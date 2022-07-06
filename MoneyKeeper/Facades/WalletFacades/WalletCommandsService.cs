using MoneyKeeper.AutoMapper.Abstractions;
using MoneyKeeper.Domain.Commands.WalletCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.WalletFacades;

internal sealed class WalletCommandsService : IWalletCommandsService
{
    private readonly IMapper _mapper;
    private readonly ICommandService<CreateWalletCommand, CreateWalletCommandResult> _createWalletService;

    public WalletCommandsService(
        IMapper mapper,
        ICommandService<CreateWalletCommand, CreateWalletCommandResult> createWalletService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _createWalletService = createWalletService ?? throw new ArgumentNullException(nameof(createWalletService));
    }

    public async Task<WalletDto?> CreateAsync(NewWalletDto dto)
    {
        Wallet wallet = _mapper.Map<NewWalletDto, Wallet>(dto)!;

        CreateWalletCommandResult result = await _createWalletService.ExecuteAsync(new CreateWalletCommand(wallet));

        return _mapper.Map<Wallet, WalletDto>(result.Data);
    }
}
