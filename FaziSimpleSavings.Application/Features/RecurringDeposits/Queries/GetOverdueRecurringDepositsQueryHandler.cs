
using Application.Interfaces;
using FaziSimpleSavings.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RecurringDeposits.Queries.GetOverdueRecurringDeposits;

public class GetOverdueRecurringDepositsQueryHandler : IRequestHandler<GetOverdueRecurringDepositsQuery, List<OverdueDepositDto>>
{
    private readonly IAppDbContext _context;

    public GetOverdueRecurringDepositsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OverdueDepositDto>> Handle(GetOverdueRecurringDepositsQuery request, CancellationToken cancellationToken)
    {
        var overdue = await _context.RecurringDeposits
            .Where(rd => rd.NextDueDate < request.NowUtc)
            .Select(rd => new OverdueDepositDto
            {
                Id = rd.Id,
                UserId = rd.UserId,
                GoalId = rd.GoalId,
                Amount = rd.Amount,
                NextDueDate = rd.NextDueDate,
                GoalName = _context.SavingsGoals
                    .Where(g => g.Id == rd.GoalId)
                    .Select(g => g.Name)
                    .FirstOrDefault()
            })
            .ToListAsync(cancellationToken);

        return overdue;
    }
}
