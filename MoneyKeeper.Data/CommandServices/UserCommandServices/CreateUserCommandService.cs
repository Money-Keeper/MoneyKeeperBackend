using MoneyKeeper.Domain.Commands.UserCommands;
using MoneyKeeper.Domain.Infrastructure.Commands;
using MoneyKeeper.Domain.Models;

namespace MoneyKeeper.Data.CommandServices.UserCommandServices;

public sealed class CreateUserCommandService : ICommandService<CreateUserCommand, CreateUserCommandResult>
{
    private readonly AppDbContext _dbContext;

    public CreateUserCommandService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<CreateUserCommandResult> ExecuteAsync(CreateUserCommand parameter)
    {
        Guid result = _dbContext.Users.Add(parameter.NewUser).Entity.Id;

        _dbContext.Wallets.Add(new Wallet { UserId = result });

        await _dbContext.SaveChangesAsync();

        return new CreateUserCommandResult(result);
    }
}
