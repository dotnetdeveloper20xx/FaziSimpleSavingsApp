
using FaziSimpleSavings.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<SavingsGoal> SavingsGoals { get; }
    DbSet<Transaction> Transactions { get; }
    DbSet<RecurringDeposit> RecurringDeposits { get; }
    DbSet<GoalCategory> GoalCategories { get; }
    DbSet<Notification> Notifications { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
