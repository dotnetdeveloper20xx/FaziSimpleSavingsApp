#nullable enable

namespace FaziSimpleSavings.Core.Entities;

public class RecurringDeposit
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; private set; }
    public Guid GoalId { get; private set; }
    public decimal Amount { get; private set; }
    public string Frequency { get; private set; }  // Could be an enum if standardized
    public DateTime NextDueDate { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public RecurringDeposit(Guid userId, Guid goalId, decimal amount, string frequency, DateTime nextDueDate)
    {
        UserId = userId != Guid.Empty ? userId : throw new ArgumentException("User ID cannot be empty.");
        GoalId = goalId != Guid.Empty ? goalId : throw new ArgumentException("Goal ID cannot be empty.");
        Amount = amount > 0 ? amount : throw new ArgumentException("Amount must be greater than zero.");
        Frequency = !string.IsNullOrWhiteSpace(frequency) ? frequency : throw new ArgumentException("Frequency cannot be empty.");
        NextDueDate = nextDueDate > DateTime.UtcNow ? nextDueDate : throw new ArgumentException("Next due date must be in the future.");
    }

    public void UpdateNextDueDate(DateTime newDueDate)
    {
        if (newDueDate <= DateTime.UtcNow) throw new ArgumentException("New due date must be in the future.");
        NextDueDate = newDueDate;
    }
}
