public class RecurringDeposit
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid GoalId { get; private set; }
    public decimal Amount { get; private set; }
    public string Frequency { get; private set; } // "Weekly", "Monthly"
    public DateTime NextDueDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public RecurringDeposit(Guid userId, Guid goalId, decimal amount, string frequency, DateTime nextDueDate)
    {
        if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty.");
        if (goalId == Guid.Empty) throw new ArgumentException("Goal ID cannot be empty.");
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

    public void UpdateNextDueDate()
    {
        NextDueDate = Frequency switch
        {
            "Weekly" => NextDueDate.AddDays(7),
            "Monthly" => NextDueDate.AddMonths(1),
            _ => throw new InvalidOperationException("Unsupported frequency: " + Frequency)
        };
    }

    public void ForceSetNextDueDate(DateTime newDueDate)
    {       
        NextDueDate = newDueDate;
    }

}
