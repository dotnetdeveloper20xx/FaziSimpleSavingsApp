using System;

namespace FaziSimpleSavings.Core.Entities
{
    public class SavingsGoal
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal TargetAmount { get; private set; }
        public decimal CurrentAmount { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Constructor with validation
        public SavingsGoal(string name, decimal targetAmount)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Goal name cannot be empty.");
            if (targetAmount <= 0) throw new ArgumentException("Target amount must be greater than zero.");

            Id = Guid.NewGuid();
            Name = name;
            TargetAmount = targetAmount;
            CurrentAmount = 0;  // Default value at the time of creation
            CreatedAt = DateTime.UtcNow;
        }

        // Method to add a deposit to the savings goal
        public void AddDeposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Deposit amount must be greater than zero.");

            CurrentAmount += amount;

            // Ensure the current amount doesn't exceed the target
            if (CurrentAmount > TargetAmount)
            {
                CurrentAmount = TargetAmount;
            }
        }

        // Method to check if goal is completed
        public bool IsGoalAchieved() => CurrentAmount >= TargetAmount;
    }
}
