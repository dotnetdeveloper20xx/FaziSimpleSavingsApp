using API.Common.Helpers;
using Application.UserSettings.Commands.UpdateUserSettings;
using Application.UserSettings.Queries.GetUserSettings;
using FaziSimpleSavings.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserSettingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserSettingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = UserContextHelper.GetUserId(User);
        var result = await _mediator.Send(new GetUserSettingsQuery(userId));

        return Ok(ApiResponse<UserSettingsDto>.Ok(result, "User settings retrieved"));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserSettingsCommand command)
    {
        var userId = UserContextHelper.GetUserId(User);
        command = command with { UserId = userId };

        await _mediator.Send(command);
        return Ok(ApiResponse<string>.Ok(null, "User settings updated successfully"));
    }
}
