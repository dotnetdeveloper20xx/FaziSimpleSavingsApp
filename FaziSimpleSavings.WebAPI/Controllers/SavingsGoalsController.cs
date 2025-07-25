﻿using Application.Features.SavingsGoals.Commands.CreateSavingsGoal;
using Application.Features.SavingsGoals.Queries.GetUserGoals;
using FaziSimpleSavings.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FaziSimpleSavings.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SavingsGoalsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SavingsGoalsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGoal([FromBody] CreateSavingsGoalCommand command)
    {
        command.UserId = this.GetUserId();

        var success = await _mediator.Send(command);
        return success ? Ok(new { message = "Goal created successfully." })
                       : BadRequest(new { message = "Goal creation failed." });
    }

    [HttpGet]
    public async Task<IActionResult> GetGoals()
    {
        var userId = this.GetUserId();
        var goals = await _mediator.Send(new GetUserGoalsQuery(userId));
        return Ok(goals);
    }
}
