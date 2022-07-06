using MoneyKeeper.Dtos;

namespace MoneyKeeper.Facades.WalletFacades;

public interface IWalletCommandsService
{
    Task<WalletDto?> CreateAsync(NewWalletDto dto);
}
