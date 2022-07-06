using MoneyKeeper.Domain.Commands.WalletCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Data.CommandServices.WalletCommandServices;

public sealed class CreateWalletCommandService : ICommandService<CreateWalletCommand, CreateWalletCommandResult>
{
    private readonly AppDbContext _dbContext;

    public CreateWalletCommandService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<CreateWalletCommandResult> ExecuteAsync(CreateWalletCommand parameter)
    {
        Wallet result = _dbContext.Wallets.Add(parameter.NewWallet).Entity;

        await _dbContext.SaveChangesAsync();

        return new CreateWalletCommandResult(result);
    }
}
