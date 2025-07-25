#nullable enable

namespace FaziSimpleSavings.Core.Entities;

public class SavingsGoal
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public decimal TargetAmount { get; private set; }
    public decimal CurrentAmount { get; private set; } = 0m;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public SavingsGoal(string name, decimal targetAmount)
    {
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Goal name cannot be empty.");
        TargetAmount = targetAmount > 0 ? targetAmount : throw new ArgumentException("Target amount must be greater than zero.");
    }

    public void AddDeposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be greater than zero.");

        CurrentAmount = Math.Min(CurrentAmount + amount, TargetAmount);
    }

    public bool IsGoalAchieved() => CurrentAmount >= TargetAmount;
}
