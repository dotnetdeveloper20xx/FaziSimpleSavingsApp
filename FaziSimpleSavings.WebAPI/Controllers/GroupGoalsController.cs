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
using System.Security.Claims;

namespace FaziSimpleSavings.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires JWT Auth
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
            // Extract UserId from JWT
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid or missing user identifier.");

            command.UserId = userId;

            var groupGoalId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = groupGoalId }, new { id = groupGoalId });
        }

        // Placeholder for GET endpoint
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok($"TODO: return group goal with ID {id}");
        }

        [HttpGet]
        public async Task<IActionResult> GetMyGroupGoals()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid or missing user identifier.");

            var result = await _mediator.Send(new GetUserGroupGoalsQuery(userId));
            return Ok(result);
        }

        [HttpPost("{id}/members")]
        public async Task<IActionResult> AddMember(Guid id, [FromBody] Guid userIdToAdd)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var requestingUserId))
                return Unauthorized("Invalid or missing user identifier.");

            var command = new AddMemberToGroupGoalCommand(id, userIdToAdd, requestingUserId);
            await _mediator.Send(command);

            return Ok(new { message = "User added to group goal." });
        }

        [HttpGet("{id}/available-users")]
        public async Task<IActionResult> GetAvailableUsers(Guid id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var requestingUserId))
                return Unauthorized("Invalid or missing user identifier.");

            var query = new GetAvailableUsersForGroupGoalQuery(id, requestingUserId);
            var users = await _mediator.Send(query);
            return Ok(users);
        }

        [HttpPost("{id}/contribute")]
        public async Task<IActionResult> Contribute(Guid id, [FromBody] decimal amount)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid or missing user identifier.");

            var command = new ContributeToGroupGoalCommand(id, amount, userId);
            await _mediator.Send(command);

            return Ok(new { message = "Contribution successful." });
        }


        [HttpGet("{id}/transactions")]
        public async Task<IActionResult> GetTransactions(Guid id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var requestingUserId))
                return Unauthorized("Invalid or missing user identifier.");

            var query = new GetGroupGoalTransactionsQuery(id, requestingUserId);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupGoalById(Guid id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var requestingUserId))
                return Unauthorized("Invalid or missing user identifier.");

            var query = new GetGroupGoalDetailsQuery(id, requestingUserId);
            var result = await _mediator.Send(query);

            return Ok(result);
        }


    }
}
