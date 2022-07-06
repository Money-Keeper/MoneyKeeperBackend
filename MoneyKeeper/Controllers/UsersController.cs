using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Dtos;
using MoneyKeeper.Facades.UserFacades;
using MoneyKeeper.Infrastructure.Attributes;
using System.Net.Mime;

namespace MoneyKeeper.Controllers;

[ApiController, Route("api/users"), Authorize, Produces(MediaTypeNames.Application.Json)]
public sealed class UsersController : ControllerBase
{
    private readonly IUserQueriesService _queriesService;

    public UsersController(IUserQueriesService queriesService)
    {
        _queriesService = queriesService ?? throw new ArgumentNullException(nameof(queriesService));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        UserDto? result = await _queriesService.GetAsync(id);

        if (result is null)
            return NotFound();

        return result;
    }
}
