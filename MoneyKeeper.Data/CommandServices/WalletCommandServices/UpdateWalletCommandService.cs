using MoneyKeeper.Domain.Commands.WalletCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Data.CommandServices.WalletCommandServices;

public sealed class UpdateWalletCommandService : ICommandService<UpdateWalletCommand, UpdateWalletCommandResult>
{
    private readonly AppDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateWalletCommandService(AppDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public async Task<UpdateWalletCommandResult> ExecuteAsync(UpdateWalletCommand parameter)
    {
        Wallet wallet = parameter.NewWallet;

        wallet.Id = parameter.Id;
        wallet.ModifiedAt = _dateTimeProvider.NowUtc;

        _dbContext.Wallets.Update(wallet).Property(x => x.UserId).IsModified = false;

        await _dbContext.SaveChangesAsync();

        return new UpdateWalletCommandResult(wallet);
    }
}
