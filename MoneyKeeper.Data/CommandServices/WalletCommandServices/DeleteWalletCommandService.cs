using MoneyKeeper.Domain.Commands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Providers.Abstractions;

namespace MoneyKeeper.Data.CommandServices.WalletCommandServices;

public sealed class DeleteWalletCommandService : ICommandService<DeleteEntityCommand<Wallet>, EmptyCommandResult>
{
    private readonly AppDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public DeleteWalletCommandService(AppDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public async Task<EmptyCommandResult> ExecuteAsync(DeleteEntityCommand<Wallet> parameter)
    {
        Wallet wallet = _dbContext.Wallets.Attach(new Wallet { Id = parameter.Id }).Entity;

        wallet.DeletedAt = _dateTimeProvider.NowUtc;

        await _dbContext.SaveChangesAsync();

        return new EmptyCommandResult();
    }
}
