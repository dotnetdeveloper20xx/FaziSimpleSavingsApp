
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetTransactionsByGoal;

public class GetTransactionsByGoalQueryHandler : IRequestHandler<GetTransactionsByGoalQuery, List<TransactionDto>>
{
    private readonly IAppDbContext _context;

    public GetTransactionsByGoalQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TransactionDto>> Handle(GetTransactionsByGoalQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _context.Transactions
            .Where(t => t.UserId == request.UserId && t.GoalId == request.GoalId)
            .OrderByDescending(t => t.TransactionDate)
            .Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate
            })
            .ToListAsync(cancellationToken);

        return transactions;
    }
}
