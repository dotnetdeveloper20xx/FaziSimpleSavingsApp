using Application.Interfaces;
using FaziSimpleSavings.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaziSimpleSavings.Test.TestUtilities;

public class TestDbContext : DbContext, IAppDbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<SavingsGoal> SavingsGoals => Set<SavingsGoal>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<RecurringDeposit> RecurringDeposits => Set<RecurringDeposit>();
    public DbSet<GoalCategory> GoalCategories => Set<GoalCategory>();
    public DbSet<Notification> Notifications => Set<Notification>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);
}
