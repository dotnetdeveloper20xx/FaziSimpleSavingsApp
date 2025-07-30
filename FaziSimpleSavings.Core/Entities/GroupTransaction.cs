using System;

namespace FaziSimpleSavings.Core.Entities
{
    public class GroupTransaction
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid GroupGoalId { get; private set; }
        public Guid UserId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime TransactionDate { get; private set; } = DateTime.UtcNow;

        public GroupTransaction(Guid groupGoalId, Guid userId, decimal amount)
        {
            if (groupGoalId == Guid.Empty)
                throw new ArgumentException("GroupGoalId is required.");
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId is required.");
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            GroupGoalId = groupGoalId;
            UserId = userId;
            Amount = amount;
        }
    }
}
