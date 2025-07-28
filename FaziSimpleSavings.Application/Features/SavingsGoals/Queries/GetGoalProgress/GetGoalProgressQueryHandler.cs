
using Application.Interfaces;
using FaziSimpleSavings.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SavingsGoals.Queries.GetGoalProgress;

public class GetGoalProgressQueryHandler : IRequestHandler<GetGoalProgressQuery, List<GoalProgressDto>>
{
    private readonly IAppDbContext _context;

    public GetGoalProgressQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<GoalProgressDto>> Handle(GetGoalProgressQuery request, CancellationToken cancellationToken)
    {
        var goals = await _context.SavingsGoals
            .Where(g => g.UserId == request.UserId)
            .Select(g => new GoalProgressDto
            {
                GoalId = g.Id,
                Name = g.Name,
                TargetAmount = g.TargetAmount,
                CurrentAmount = g.CurrentAmount,
                ProgressPercentage = g.TargetAmount == 0
                    ? 0
                    : (int)((g.CurrentAmount / g.TargetAmount) * 100),
                IsGoalAchieved = g.CurrentAmount >= g.TargetAmount
            })
            .ToListAsync(cancellationToken);

        return goals;
    }
}
