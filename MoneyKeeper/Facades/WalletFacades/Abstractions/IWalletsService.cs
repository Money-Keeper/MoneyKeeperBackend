using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.WalletFacades.Abstractions;

public interface IWalletsService
{
    Task<WalletDto> CreateAsync(NewWalletDto dto);
    Task<WalletDto> UpdateAsync(Guid id, NewWalletDto dto);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<WalletDto?> GetAsync(Guid id);
    Task<DataResult<WalletDto>> GetAsync();
}
