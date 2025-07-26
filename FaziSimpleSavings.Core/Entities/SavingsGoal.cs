#nullable enable

namespace FaziSimpleSavings.Core.Entities;

public class SavingsGoal
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public decimal TargetAmount { get; private set; }
    public decimal CurrentAmount { get; private set; } = 0m;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;


    public SavingsGoal(string name, decimal targetAmount, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.");
        if (targetAmount <= 0) throw new ArgumentException("Target amount must be greater than zero.");
        if (userId == Guid.Empty) throw new ArgumentException("UserId is required.");

        Id = Guid.NewGuid();
        Name = name;
        TargetAmount = targetAmount;
        CurrentAmount = 0;
        CreatedAt = DateTime.UtcNow;
        UserId = userId;
    }

    public void AddDeposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Deposit amount must be greater than zero.");

        CurrentAmount += amount;

        if (CurrentAmount > TargetAmount)
        {
            CurrentAmount = TargetAmount;
        }
    }



    public bool IsGoalAchieved() => CurrentAmount >= TargetAmount;
}
