using Application.Transactions.Queries.GetTransactionsByGoal;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Common.Helpers;
using Application.Features.SavingsGoals.Commands.CreateSavingsGoal;
using Application.Features.SavingsGoals.Queries.GetUserGoals;

namespace API.Controllers;

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
        command.UserId = UserContextHelper.GetUserId(User);

        var success = await _mediator.Send(command);
        return success
            ? Ok(new { message = "Goal created successfully." })
            : BadRequest(new { message = "Goal creation failed." });
    }

    [HttpGet]
    public async Task<IActionResult> GetGoals()
    {
        var userId = UserContextHelper.GetUserId(User);
        var goals = await _mediator.Send(new GetUserGoalsQuery(userId));
        return Ok(goals);
    }

    [HttpGet("{goalId}/transactions")]
    public async Task<IActionResult> GetTransactions(Guid goalId)
    {
        var userId = UserContextHelper.GetUserId(User);
        var result = await _mediator.Send(new GetTransactionsByGoalQuery(userId, goalId));
        return Ok(result);
    }
}
