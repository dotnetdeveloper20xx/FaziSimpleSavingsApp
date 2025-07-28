using API.Common.Helpers;
using Application.Features.SavingsGoals.Commands.CreateSavingsGoal;
using Application.Features.SavingsGoals.Queries.GetUserGoals;
using Application.SavingsGoals.Queries.GetGoalProgress;
using Application.Transactions.Commands.CreateManualTransaction;
using Application.Transactions.Queries.GetTransactionsByGoal;
using FaziSimpleSavings.Application.Common.Exceptions;
using FaziSimpleSavings.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

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
        var userId = UserContextHelper.GetUserId(User);
        command = command with { UserId = userId };
        var goalId = await _mediator.Send(command);

        return Ok(ApiResponse<Guid>.Ok(goalId, "Goal created successfully"));
    }

    [HttpGet]
    public async Task<IActionResult> GetGoals()
    {
        var userId = UserContextHelper.GetUserId(User);
        var goals = await _mediator.Send(new GetUserGoalsQuery(userId));

        return Ok(ApiResponse<List<SavingsGoalDto>>.Ok(goals, "User goals retrieved"));
    }

    [HttpGet("progress")]
    public async Task<IActionResult> GetGoalProgress()
    {
        var userId = UserContextHelper.GetUserId(User);
        var result = await _mediator.Send(new GetGoalProgressQuery(userId));

        return Ok(ApiResponse<List<GoalProgressDto>>.Ok(result, "Goal progress calculated"));

    }

    [HttpPost("{goalId}/deposit")]
    public async Task<IActionResult> DepositToGoal(Guid goalId, [FromBody] decimal amount)
    {
        // Amount validation should be handled by FluentValidation instead
        var userId = UserContextHelper.GetUserId(User);
        var command = new CreateManualTransactionCommand(userId, goalId, amount);

        await _mediator.Send(command);
        return Ok(ApiResponse<string>.Ok(null, $"£{amount} was deposited successfully"));
    }

    [HttpGet("{goalId}/transactions")]
    public async Task<IActionResult> GetTransactions(Guid goalId)
    {
        var userId = UserContextHelper.GetUserId(User);
        var result = await _mediator.Send(new GetTransactionsByGoalQuery(userId, goalId));

        return Ok(ApiResponse<List<TransactionDto>>.Ok(result, "Goal transactions retrieved"));
    }
}
