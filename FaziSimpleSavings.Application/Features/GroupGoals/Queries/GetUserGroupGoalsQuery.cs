using MediatR;
using System;
using System.Collections.Generic;

namespace FaziSimpleSavings.Application.GroupGoals.Queries.GetUserGroupGoals
{
    public class GetUserGroupGoalsQuery : IRequest<List<GroupGoalDto>>
    {
        public Guid UserId { get; set; }

        public GetUserGroupGoalsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
