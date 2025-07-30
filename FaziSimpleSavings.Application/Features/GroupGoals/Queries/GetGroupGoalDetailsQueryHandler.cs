using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace FaziSimpleSavings.Application.GroupGoals.Queries.GetGroupGoalDetails
{
    public class GetGroupGoalDetailsQueryHandler : IRequestHandler<GetGroupGoalDetailsQuery, GroupGoalDetailsDto>
    {
        private readonly IAppDbContext _context;

        public GetGroupGoalDetailsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<GroupGoalDetailsDto> Handle(GetGroupGoalDetailsQuery request, CancellationToken cancellationToken)
        {
            var isMember = await _context.GroupGoalMembers
                .AnyAsync(m => m.GroupGoalId == request.GroupGoalId && m.UserId == request.RequestingUserId, cancellationToken);

            if (!isMember)
                throw new UnauthorizedAccessException("You are not a member of this group goal.");

            var goal = await _context.GroupSavingsGoals
                .Include(g => g.Members)
                .FirstOrDefaultAsync(g => g.Id == request.GroupGoalId, cancellationToken);

            if (goal == null)
                throw new Exception("Group goal not found.");

            var createdByUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == goal.CreatedByUserId, cancellationToken);

            var memberDtos = await _context.GroupGoalMembers
                .Where(m => m.GroupGoalId == request.GroupGoalId)
                .Join(_context.Users,
                      m => m.UserId,
                      u => u.Id,
                      (m, u) => new GroupGoalMemberDto
                      {
                          UserId = u.Id,
                          FullName = $"{u.FirstName} {u.LastName}",
                          ContributedAmount = m.ContributedAmount
                      })
                .ToListAsync(cancellationToken);

            return new GroupGoalDetailsDto
            {
                Id = goal.Id,
                Name = goal.Name,
                TargetAmount = goal.TargetAmount,
                TotalSaved = goal.TotalSaved,
                CreatedAt = goal.CreatedAt,
                CreatedByName = createdByUser != null ? $"{createdByUser.FirstName} {createdByUser.LastName}" : "Unknown",
                Members = memberDtos
            };
        }
    }
}
