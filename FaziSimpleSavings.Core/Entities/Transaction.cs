#nullable enable

namespace FaziSimpleSavings.Core.Entities;

public class Transaction
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; private set; }
    public Guid GoalId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime TransactionDate { get; private set; } = DateTime.UtcNow;

    public Transaction(Guid userId, Guid goalId, decimal amount)
    {
        if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty.");
        if (goalId == Guid.Empty) throw new ArgumentException("Goal ID cannot be empty.");
        if (amount <= 0) throw new ArgumentException("Transaction amount must be greater than zero.");

        UserId = userId;
        GoalId = goalId;
        Amount = amount;
    }
}
