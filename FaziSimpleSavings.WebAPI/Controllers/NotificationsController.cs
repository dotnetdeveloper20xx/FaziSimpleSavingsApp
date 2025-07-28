using Application.Notifications.Commands.MarkNotificationAsRead;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Common.Helpers;
using FaziSimpleSavings.Application.Notifications.Queries;
using FaziSimpleSavings.Application.Common.Exceptions;
using FaziSimpleSavings.Application.Dtos;

namespace FaziSimpleSavings.WebAPI.Controllers;

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

        var result = await _mediator.Send(new GetUserNotificationsQuery(userId));
        return Ok(ApiResponse<List<NotificationDto>>.Ok(result, "Notifications retrieved"));
    }

    [HttpPost("{id}/mark-as-read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        var userId = UserContextHelper.GetUserId(User);

        await _mediator.Send(new MarkNotificationAsReadCommand(id, userId));

        return Ok(ApiResponse<string>.Ok(data: null, $"Notification {id} marked as read"));
    }
}

