using Application.Notifications.Commands.MarkNotificationAsRead;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Common.Helpers;
using FaziSimpleSavings.Application.Notifications.Queries;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserNotifications()
    {
        var userId = UserContextHelper.GetUserId(User);
        var notifications = await _mediator.Send(new GetUserNotificationsQuery(userId));
        return Ok(notifications);
    }

    [HttpPost("{id}/mark-as-read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        var userId = UserContextHelper.GetUserId(User);
        var result = await _mediator.Send(new MarkNotificationAsReadCommand(id, userId));

        if (!result)
            return NotFound();

        return NoContent();
    }
}
