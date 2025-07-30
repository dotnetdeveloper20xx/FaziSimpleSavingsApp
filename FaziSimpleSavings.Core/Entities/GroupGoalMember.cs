using System;

namespace FaziSimpleSavings.Core.Entities
{
    public class GroupGoalMember
    {
        public Guid Id { get; private set; } = Guid.NewGuid();       
        public Guid UserId { get; private set; }
        public decimal ContributedAmount { get; private set; } = 0m;
        public Guid GroupGoalId { get; private set; }
        public GroupSavingsGoal GroupGoal { get; private set; } = null!;


        public DateTime JoinedAt { get; private set; } = DateTime.UtcNow;

        public GroupGoalMember(Guid groupGoalId, Guid userId)
        {
            if (groupGoalId == Guid.Empty)
                throw new ArgumentException("GroupGoalId is required.");
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId is required.");

            GroupGoalId = groupGoalId;
            UserId = userId;
        }

        public void AddContribution(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            ContributedAmount += amount;
        }
    }
}
