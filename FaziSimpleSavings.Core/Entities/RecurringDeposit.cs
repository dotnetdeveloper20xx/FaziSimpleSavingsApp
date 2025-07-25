public class RecurringDeposit
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid GoalId { get; private set; }
    public decimal Amount { get; private set; }
    public string Frequency { get; private set; } // e.g., "Weekly", "Monthly"
    public DateTime NextDueDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public RecurringDeposit(Guid userId, Guid goalId, decimal amount, string frequency, DateTime nextDueDate)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.");
        if (string.IsNullOrWhiteSpace(frequency)) throw new ArgumentException("Frequency cannot be empty.");
        if (nextDueDate < DateTime.UtcNow) throw new ArgumentException("Next due date must be in the future.");

        Id = Guid.NewGuid();
        UserId = userId;
        GoalId = goalId;
        Amount = amount;
        Frequency = frequency;
        NextDueDate = nextDueDate;
        CreatedAt = DateTime.UtcNow;
    }

    // Method to update the due date after deposit is made
    public void UpdateNextDueDate(DateTime newDueDate)
    {
        if (newDueDate < DateTime.UtcNow) throw new ArgumentException("New due date must be in the future.");
        NextDueDate = newDueDate;
    }
}
