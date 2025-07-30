using MediatR;
using System;

namespace FaziSimpleSavings.Application.GroupGoals.Commands.ContributeToGroupGoal
{
    public class ContributeToGroupGoalCommand : IRequest<bool>
    {
        public Guid GroupGoalId { get; set; }
        public decimal Amount { get; set; }
        public Guid UserId { get; set; } // from JWT

        public ContributeToGroupGoalCommand(Guid groupGoalId, decimal amount, Guid userId)
        {
            GroupGoalId = groupGoalId;
            Amount = amount;
            UserId = userId;
        }
    }
}
