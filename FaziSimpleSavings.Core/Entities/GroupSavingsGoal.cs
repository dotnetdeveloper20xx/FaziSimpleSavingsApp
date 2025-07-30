using System;
using System.Collections.Generic;
using System.Linq;

namespace FaziSimpleSavings.Core.Entities
{
    public class GroupSavingsGoal
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public decimal TargetAmount { get; private set; }
        public decimal TotalSaved { get; private set; } = 0m;
        public Guid CreatedByUserId { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        // Navigation property to members
        private readonly List<GroupGoalMember> _members = new();
        public IReadOnlyCollection<GroupGoalMember> Members => _members.AsReadOnly();

        public GroupSavingsGoal(string name, decimal targetAmount, Guid createdByUserId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Goal name cannot be empty.");
            if (targetAmount <= 0)
                throw new ArgumentException("Target amount must be greater than zero.");
            if (createdByUserId == Guid.Empty)
                throw new ArgumentException("CreatedByUserId is required.");

            Name = name;
            TargetAmount = targetAmount;
            CreatedByUserId = createdByUserId;
        }

        public void AddMember(GroupGoalMember member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            if (_members.Any(m => m.UserId == member.UserId))
                throw new InvalidOperationException("User is already a member of this group goal.");

            _members.Add(member);
        }

        public void AddContribution(Guid userId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Contribution must be greater than zero.");

            var member = _members.FirstOrDefault(m => m.UserId == userId);
            if (member == null)
                throw new InvalidOperationException("User is not a member of this group goal.");

            member.AddContribution(amount);
            TotalSaved += amount;
        }

        public bool IsGoalAchieved() => TotalSaved >= TargetAmount;
    }
}
