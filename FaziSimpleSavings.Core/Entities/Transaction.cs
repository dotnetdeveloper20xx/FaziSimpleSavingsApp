using System;

namespace FaziSimpleSavings.Core.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid GoalId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime TransactionDate { get; private set; }

        // Constructor with validation
        public Transaction(Guid userId, Guid goalId, decimal amount)
        {
            if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty.");
            if (goalId == Guid.Empty) throw new ArgumentException("Goal ID cannot be empty.");
            if (amount <= 0) throw new ArgumentException("Transaction amount must be greater than zero.");

            Id = Guid.NewGuid();
            UserId = userId;
            GoalId = goalId;
            Amount = amount;
            TransactionDate = DateTime.UtcNow;
        }
    }
}
