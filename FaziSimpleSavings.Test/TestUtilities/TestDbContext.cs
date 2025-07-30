using Application.Interfaces;
using FaziSimpleSavings.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaziSimpleSavings.Test.TestUtilities;

public class TestDbContext : DbContext, IAppDbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<SavingsGoal> SavingsGoals => Set<SavingsGoal>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<RecurringDeposit> RecurringDeposits => Set<RecurringDeposit>();
    public DbSet<GoalCategory> GoalCategories => Set<GoalCategory>();
    public DbSet<Notification> Notifications => Set<Notification>();

    public DbSet<UserSettings> UserSettings => Set<UserSettings>();

    public DbSet<GroupSavingsGoal> GroupSavingsGoals => Set<GroupSavingsGoal>();

    public DbSet<GroupGoalMember> GroupGoalMembers => Set<GroupGoalMember>();

    public DbSet<GroupTransaction> GroupTransactions => Set<GroupTransaction>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);
}
