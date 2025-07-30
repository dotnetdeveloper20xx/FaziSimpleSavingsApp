using MediatR;
using System;

namespace FaziSimpleSavings.Application.GroupGoals.Commands.AddMemberToGroupGoal
{
    public class AddMemberToGroupGoalCommand : IRequest<bool>
    {
        public Guid GroupGoalId { get; set; }
        public Guid UserIdToAdd { get; set; }
        public Guid RequestingUserId { get; set; } // from JWT

        public AddMemberToGroupGoalCommand(Guid groupGoalId, Guid userIdToAdd, Guid requestingUserId)
        {
            GroupGoalId = groupGoalId;
            UserIdToAdd = userIdToAdd;
            RequestingUserId = requestingUserId;
        }
    }
}
