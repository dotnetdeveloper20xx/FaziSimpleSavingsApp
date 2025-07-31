using FaziSimpleSavings.Application.Features.GroupGoals.Commands;
using FaziSimpleSavings.Application.GroupGoals.Commands.AddMemberToGroupGoal;
using FaziSimpleSavings.Application.GroupGoals.Commands.ContributeToGroupGoal;
using FaziSimpleSavings.Application.GroupGoals.Queries.GetAvailableUsersForGroupGoal;
using FaziSimpleSavings.Application.GroupGoals.Queries.GetGroupGoalDetails;
using FaziSimpleSavings.Application.GroupGoals.Queries.GetGroupGoalTransactions;
using FaziSimpleSavings.Application.GroupGoals.Queries.GetUserGroupGoals;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Common.Helpers;
using FaziSimpleSavings.Application.Common.Exceptions;

namespace FaziSimpleSavings.API.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/group-goals")]
    [Authorize]
    public class GroupGoalsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupGoalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGroupSavingsGoalCommand command)
        {
            command.UserId = UserContextHelper.GetUserId(User);
            var id = await _mediator.Send(command);
            var response = ApiResponse<Guid>.Ok(id, "Group goal created successfully", 201);
            return StatusCode(201, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyGroupGoals()
        {
            var result = await _mediator.Send(new GetUserGroupGoalsQuery(UserContextHelper.GetUserId(User)));
            return Ok(ApiResponse<object>.Ok(result));
        }

        [HttpPost("{id}/members")]
        public async Task<IActionResult> AddMember(Guid id, [FromBody] Guid userIdToAdd)
        {
            var command = new AddMemberToGroupGoalCommand(id, userIdToAdd, UserContextHelper.GetUserId(User));
            await _mediator.Send(command);
            return Ok(ApiResponse<string>.Ok("User added to group goal."));
        }

        [HttpGet("{id}/available-users")]
        public async Task<IActionResult> GetAvailableUsers(Guid id)
        {
            var query = new GetAvailableUsersForGroupGoalQuery(id, UserContextHelper.GetUserId(User));
            var users = await _mediator.Send(query);
            return Ok(ApiResponse<object>.Ok(users));
        }

        [HttpPost("{id}/contribute")]
        public async Task<IActionResult> Contribute(Guid id, [FromBody] decimal amount)
        {
            var command = new ContributeToGroupGoalCommand(id, amount, UserContextHelper.GetUserId(User));
            await _mediator.Send(command);
            return Ok(ApiResponse<string>.Ok("Contribution successful."));
        }

        [HttpGet("{id}/transactions")]
        public async Task<IActionResult> GetTransactions(Guid id)
        {
            var query = new GetGroupGoalTransactionsQuery(id, UserContextHelper.GetUserId(User));
            var result = await _mediator.Send(query);
            return Ok(ApiResponse<object>.Ok(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupGoalById(Guid id)
        {
            var query = new GetGroupGoalDetailsQuery(id, UserContextHelper.GetUserId(User));
            var result = await _mediator.Send(query);
            return Ok(ApiResponse<object>.Ok(result));
        }
    }
}
