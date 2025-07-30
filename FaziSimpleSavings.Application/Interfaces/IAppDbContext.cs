
using FaziSimpleSavings.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<SavingsGoal> SavingsGoals { get; }
    DbSet<Transaction> Transactions { get; }
    DbSet<RecurringDeposit> RecurringDeposits { get; }
    DbSet<GoalCategory> GoalCategories { get; }
    DbSet<Notification> Notifications { get; }
    DbSet<FaziSimpleSavings.Core.Entities.UserSettings> UserSettings { get; }
    DbSet<GroupSavingsGoal> GroupSavingsGoals { get; }
    DbSet<GroupGoalMember> GroupGoalMembers { get; }
    DbSet<GroupTransaction> GroupTransactions { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
