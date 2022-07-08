using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Controllers.Abstractions;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.WalletFacades.Abstractions;

namespace MoneyKeeper.Controllers;

[Route("api/wallets")]
public sealed class WalletsController : BaseController
{
    private readonly IWalletsService _walletsService;

    public WalletsController(IWalletsService walletsService)
    {
        _walletsService = walletsService ?? throw new ArgumentNullException(nameof(walletsService));
    }

    [HttpGet]
    public Task<DataResult<WalletDto>> Get() => _walletsService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<WalletDto>> Get(Guid id)
    {
        WalletDto? result = await _walletsService.GetAsync(id);

        if (result is null)
            return NotFound();

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<WalletDto>> Post(NewWalletDto dto)
    {
        return await _walletsService.CreateAsync(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WalletDto>> Put(Guid id, NewWalletDto dto)
    {
        bool exists = await _walletsService.ExistsAsync(id);

        if (!exists)
            return NotFound();

        return await _walletsService.UpdateAsync(id, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool exists = await _walletsService.ExistsAsync(id);

        if (!exists)
            return NotFound();

        await _walletsService.DeleteAsync(id);

        return NoContent();
    }
}
