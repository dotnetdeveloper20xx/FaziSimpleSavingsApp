using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FaziSimpleSavings.Application.GroupGoals.Queries.GetAvailableUsersForGroupGoal
{
    public class GetAvailableUsersForGroupGoalQueryHandler
        : IRequestHandler<GetAvailableUsersForGroupGoalQuery, List<UserDto>>
    {
        private readonly IAppDbContext _context;

        public GetAvailableUsersForGroupGoalQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> Handle(
            GetAvailableUsersForGroupGoalQuery request,
            CancellationToken cancellationToken)
        {
            // 1. Fetch the group goal
            var group = await _context.GroupSavingsGoals
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == request.GroupGoalId, cancellationToken);

            if (group == null)
                throw new Exception("Group savings goal not found.");

            // 2. Ensure the requester is the creator
            if (group.CreatedByUserId != request.RequestingUserId)
                throw new UnauthorizedAccessException("Only the creator can invite users to this group goal.");

            // 3. Get user IDs already in the group
            var memberUserIds = await _context.GroupGoalMembers
                .Where(m => m.GroupGoalId == request.GroupGoalId)
                .Select(m => m.UserId)
                .ToListAsync(cancellationToken);

            // 4. Return users not in the group and not the creator
            var availableUsers = await _context.Users
                .Where(u => !memberUserIds.Contains(u.Id) && u.Id != request.RequestingUserId)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FullName = $"{u.FirstName} {u.LastName}",
                    Email = u.Email
                })
                .ToListAsync(cancellationToken);

            return availableUsers;
        }
    }
}
