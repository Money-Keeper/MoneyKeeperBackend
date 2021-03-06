using MoneyKeeper.Domain.Infrastructure.Queries;
using MoneyKeeper.Domain.Models;
using MoneyKeeper.Domain.Queries.WalletQueries;
using MoneyKeeper.Facades.WalletFacades.Abstractions;

namespace MoneyKeeper.Facades.WalletFacades;

internal sealed class WalletQueries : IWalletQueries
{
    private readonly IQueryService<WalletExistsQuery, bool> _walletExistsService;
    private readonly IQueryService<GetWalletByIdQuery, Wallet?> _getWalletByIdService;
    private readonly IQueryService<GetWalletsQuery, IEnumerable<Wallet>> _getWalletsByConditionService;

    public WalletQueries(
        IQueryService<WalletExistsQuery, bool> walletExistsService,
        IQueryService<GetWalletByIdQuery, Wallet?> getWalletByIdService,
        IQueryService<GetWalletsQuery, IEnumerable<Wallet>> getWalletsByConditionService)
    {
        _walletExistsService = walletExistsService ?? throw new ArgumentNullException(nameof(walletExistsService));
        _getWalletByIdService = getWalletByIdService ?? throw new ArgumentNullException(nameof(getWalletByIdService));
        _getWalletsByConditionService = getWalletsByConditionService ?? throw new ArgumentNullException(nameof(getWalletsByConditionService));
    }

    public Task<bool> ExistsAsync(Guid id, Guid userId)
    {
        return _walletExistsService.ExecuteAsync(new WalletExistsQuery(id, userId));
    }

    public Task<Wallet?> GetAsync(Guid id, Guid userId)
    {
        return _getWalletByIdService.ExecuteAsync(new GetWalletByIdQuery(id, userId));
    }

    public Task<IEnumerable<Wallet>> GetAsync(Guid userId)
    {
        return _getWalletsByConditionService.ExecuteAsync(new GetWalletsQuery(userId));
    }
}
