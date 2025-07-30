using MediatR;
using System;
using System.Collections.Generic;

namespace FaziSimpleSavings.Application.GroupGoals.Queries.GetAvailableUsersForGroupGoal
{
    public class GetAvailableUsersForGroupGoalQuery : IRequest<List<UserDto>>
    {
        public Guid GroupGoalId { get; set; }
        public Guid RequestingUserId { get; set; }

        public GetAvailableUsersForGroupGoalQuery(Guid groupGoalId, Guid requestingUserId)
        {
            GroupGoalId = groupGoalId;
            RequestingUserId = requestingUserId;
        }
    }
}
