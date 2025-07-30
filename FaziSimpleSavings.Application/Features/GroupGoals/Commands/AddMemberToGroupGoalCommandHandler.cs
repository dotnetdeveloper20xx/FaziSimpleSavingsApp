using Application.Interfaces;
using FaziSimpleSavings.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace FaziSimpleSavings.Application.GroupGoals.Commands.AddMemberToGroupGoal
{
    public class AddMemberToGroupGoalCommandHandler : IRequestHandler<AddMemberToGroupGoalCommand, bool>
    {
        private readonly IAppDbContext _context;

        public AddMemberToGroupGoalCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddMemberToGroupGoalCommand request, CancellationToken cancellationToken)
        {
            var group = await _context.GroupSavingsGoals
                .FirstOrDefaultAsync(g => g.Id == request.GroupGoalId, cancellationToken);

            if (group == null)
                throw new Exception("Group goal not found.");

            if (group.CreatedByUserId != request.RequestingUserId)
                throw new UnauthorizedAccessException("Only the goal creator can add members.");

            var alreadyMember = await _context.GroupGoalMembers
                .AnyAsync(m => m.GroupGoalId == request.GroupGoalId && m.UserId == request.UserIdToAdd, cancellationToken);

            if (alreadyMember)
                throw new InvalidOperationException("User is already a member.");

            var member = new GroupGoalMember(request.GroupGoalId, request.UserIdToAdd);
            _context.GroupGoalMembers.Add(member);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
