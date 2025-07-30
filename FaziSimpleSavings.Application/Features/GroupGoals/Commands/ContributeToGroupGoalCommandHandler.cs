using Application.Interfaces;
using FaziSimpleSavings.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace FaziSimpleSavings.Application.GroupGoals.Commands.ContributeToGroupGoal
{
    public class ContributeToGroupGoalCommandHandler : IRequestHandler<ContributeToGroupGoalCommand, bool>
    {
        private readonly IAppDbContext _context;

        public ContributeToGroupGoalCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ContributeToGroupGoalCommand request, CancellationToken cancellationToken)
        {
            if (request.Amount <= 0)
                throw new ArgumentException("Contribution must be greater than zero.");

            var goal = await _context.GroupSavingsGoals
                .FirstOrDefaultAsync(g => g.Id == request.GroupGoalId, cancellationToken);

            if (goal == null)
                throw new Exception("Group goal not found.");

            var member = await _context.GroupGoalMembers
                .FirstOrDefaultAsync(m => m.GroupGoalId == request.GroupGoalId && m.UserId == request.UserId, cancellationToken);

            if (member == null)
                throw new UnauthorizedAccessException("You are not a member of this group goal.");

            // Update in-memory contribution state
            member.AddContribution(request.Amount);
            goal.AddContribution(request.UserId, request.Amount);

            // Record the transaction
            var transaction = new GroupTransaction(request.GroupGoalId, request.UserId, request.Amount);
            _context.GroupTransactions.Add(transaction);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
