using Application.Interfaces;
using FaziSimpleSavings.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SavingsGoals.Queries.GetUserGoals;

public class GetUserGoalsQueryHandler : IRequestHandler<GetUserGoalsQuery, List<SavingsGoalDto>>
{
    private readonly IAppDbContext _context;

    public GetUserGoalsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SavingsGoalDto>> Handle(GetUserGoalsQuery request, CancellationToken cancellationToken)
    {
        return await _context.SavingsGoals
            .Where(g => g.UserId == request.UserId)
            .Select(g => new SavingsGoalDto
            {
                Id = g.Id,
                Name = g.Name,
                TargetAmount = g.TargetAmount,
                CurrentAmount = g.CurrentAmount,
                CreatedAt = g.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
