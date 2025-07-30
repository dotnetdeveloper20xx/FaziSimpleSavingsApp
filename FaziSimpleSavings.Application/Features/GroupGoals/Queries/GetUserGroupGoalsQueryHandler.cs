using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace FaziSimpleSavings.Application.GroupGoals.Queries.GetUserGroupGoals
{
    public class GetUserGroupGoalsQueryHandler : IRequestHandler<GetUserGroupGoalsQuery, List<GroupGoalDto>>
    {
        private readonly IAppDbContext _context;

        public GetUserGroupGoalsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GroupGoalDto>> Handle(GetUserGroupGoalsQuery request, CancellationToken cancellationToken)
        {
            var groupGoals = await _context.GroupGoalMembers
                .Where(m => m.UserId == request.UserId)
                .Include(m => m.GroupGoal) // assuming navigation property for future-proofing
                .Select(m => new GroupGoalDto
                {
                    Id = m.GroupGoalId,
                    Name = _context.GroupSavingsGoals.Where(g => g.Id == m.GroupGoalId).Select(g => g.Name).FirstOrDefault(),
                    TargetAmount = _context.GroupSavingsGoals.Where(g => g.Id == m.GroupGoalId).Select(g => g.TargetAmount).FirstOrDefault(),
                    TotalSaved = _context.GroupSavingsGoals.Where(g => g.Id == m.GroupGoalId).Select(g => g.TotalSaved).FirstOrDefault(),
                    MyContribution = m.ContributedAmount
                })
                .ToListAsync(cancellationToken);

            return groupGoals;
        }
    }
}
