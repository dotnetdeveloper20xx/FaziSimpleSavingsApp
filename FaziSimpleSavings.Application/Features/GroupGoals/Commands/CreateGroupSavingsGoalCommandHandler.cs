using Application.Interfaces;
using FaziSimpleSavings.Core.Entities;
using MediatR;


namespace FaziSimpleSavings.Application.Features.GroupGoals.Commands
{
    public class CreateGroupSavingsGoalCommandHandler : IRequestHandler<CreateGroupSavingsGoalCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateGroupSavingsGoalCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateGroupSavingsGoalCommand request, CancellationToken cancellationToken)
        {
            var groupGoal = new GroupSavingsGoal(request.Name, request.TargetAmount, request.UserId);

            var member = new GroupGoalMember(groupGoal.Id, request.UserId);
            groupGoal.AddMember(member);

            _context.GroupSavingsGoals.Add(groupGoal);
            _context.GroupGoalMembers.Add(member);

            await _context.SaveChangesAsync(cancellationToken);

            return groupGoal.Id;
        }
    }
}
