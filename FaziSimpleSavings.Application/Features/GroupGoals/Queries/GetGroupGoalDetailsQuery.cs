using MediatR;
using System;

namespace FaziSimpleSavings.Application.GroupGoals.Queries.GetGroupGoalDetails
{
    public class GetGroupGoalDetailsQuery : IRequest<GroupGoalDetailsDto>
    {
        public Guid GroupGoalId { get; set; }
        public Guid RequestingUserId { get; set; }

        public GetGroupGoalDetailsQuery(Guid groupGoalId, Guid requestingUserId)
        {
            GroupGoalId = groupGoalId;
            RequestingUserId = requestingUserId;
        }
    }
}
