using MoneyKeeper.Domain.Commands;
using MoneyKeeper.Domain.Commands.WalletCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Facades.WalletFacades.Abstractions;

namespace MoneyKeeper.Facades.WalletFacades;

internal sealed class WalletCommands : IWalletCommands
{
    private readonly ICommandService<CreateWalletCommand, CreateWalletCommandResult> _createWalletService;
    private readonly ICommandService<UpdateWalletCommand, UpdateWalletCommandResult> _updateWalletService;
    private readonly ICommandService<DeleteEntityCommand<Wallet>, EmptyCommandResult> _deleteWalletService;

    public WalletCommands(
        ICommandService<CreateWalletCommand, CreateWalletCommandResult> createWalletService,
        ICommandService<UpdateWalletCommand, UpdateWalletCommandResult> updateWalletService,
        ICommandService<DeleteEntityCommand<Wallet>, EmptyCommandResult> deleteWalletService)
    {
        _createWalletService = createWalletService ?? throw new ArgumentNullException(nameof(createWalletService));
        _updateWalletService = updateWalletService ?? throw new ArgumentNullException(nameof(updateWalletService));
        _deleteWalletService = deleteWalletService ?? throw new ArgumentNullException(nameof(deleteWalletService));
    }

    public async Task<Wallet> CreateAsync(Wallet wallet, Guid userId)
    {
        return (await _createWalletService.ExecuteAsync(new CreateWalletCommand(wallet, userId))).Data;
    }

    public Task DeleteAsync(Guid id)
    {
        return _deleteWalletService.ExecuteAsync(new DeleteEntityCommand<Wallet>(id));
    }

    public async Task<Wallet> UpdateAsync(Guid id, Wallet wallet)
    {
        return (await _updateWalletService.ExecuteAsync(new UpdateWalletCommand(id, wallet))).Data;
    }
}
