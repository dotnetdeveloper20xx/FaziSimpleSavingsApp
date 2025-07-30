using MediatR;
using System;
using System.Collections.Generic;

namespace FaziSimpleSavings.Application.GroupGoals.Queries.GetGroupGoalTransactions
{
    public class GetGroupGoalTransactionsQuery : IRequest<List<GroupTransactionDto>>
    {
        public Guid GroupGoalId { get; set; }
        public Guid RequestingUserId { get; set; }

        public GetGroupGoalTransactionsQuery(Guid groupGoalId, Guid requestingUserId)
        {
            GroupGoalId = groupGoalId;
            RequestingUserId = requestingUserId;
        }
    }
}
