using MediatR;
using System;

namespace FaziSimpleSavings.Application.Features.GroupGoals.Commands
{
    public class CreateGroupSavingsGoalCommand : IRequest<Guid>  // returns new GroupGoal Id
    {
        public string Name { get; set; } = null!;
        public decimal TargetAmount { get; set; }
        public Guid UserId { get; set; } // passed from controller/User context
    }
}
