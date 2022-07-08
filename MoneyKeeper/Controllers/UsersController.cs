using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Controllers.Abstractions;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.UserFacades.Abstractions;

namespace MoneyKeeper.Controllers;

[Route("api/users")]
public sealed class UsersController : BaseController
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
    }

    [HttpGet]
    public async Task<ActionResult<UserDto>> Get()
    {
        UserDto? result = await _usersService.GetAsync();

        if (result is null)
            return NotFound();

        return result;
    }
}
