
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace FaziSimpleSavings.Application.GroupGoals.Queries.GetGroupGoalTransactions
{
    public class GetGroupGoalTransactionsQueryHandler : IRequestHandler<GetGroupGoalTransactionsQuery, List<GroupTransactionDto>>
    {
        private readonly IAppDbContext _context;

        public GetGroupGoalTransactionsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GroupTransactionDto>> Handle(GetGroupGoalTransactionsQuery request, CancellationToken cancellationToken)
        {
            // Verify user is a member
            var isMember = await _context.GroupGoalMembers
                .AnyAsync(m => m.GroupGoalId == request.GroupGoalId && m.UserId == request.RequestingUserId, cancellationToken);

            if (!isMember)
                throw new UnauthorizedAccessException("You are not a member of this group goal.");

            var transactions = await _context.GroupTransactions
                .Where(t => t.GroupGoalId == request.GroupGoalId)
                .OrderByDescending(t => t.TransactionDate)
                .Join(_context.Users,
                    tx => tx.UserId,
                    user => user.Id,
                    (tx, user) => new GroupTransactionDto
                    {
                        UserFullName = $"{user.FirstName} {user.LastName}",
                        Amount = tx.Amount,
                        Date = tx.TransactionDate
                    })
                .ToListAsync(cancellationToken);

            return transactions;
        }
    }
}
