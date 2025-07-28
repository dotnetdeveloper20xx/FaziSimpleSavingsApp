using Application.Interfaces;
using FaziSimpleSavings.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<SavingsGoal> SavingsGoals => Set<SavingsGoal>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<RecurringDeposit> RecurringDeposits => Set<RecurringDeposit>();
    public DbSet<GoalCategory> GoalCategories => Set<GoalCategory>();
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<UserSettings> UserSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRole>()
         .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        modelBuilder.Entity<SavingsGoal>()
                    .Property(g => g.TargetAmount)
                    .HasPrecision(18, 2);

        modelBuilder.Entity<SavingsGoal>()
                    .Property(g => g.CurrentAmount)
                    .HasPrecision(18, 2);

        modelBuilder.Entity<RecurringDeposit>()
                    .Property(rd => rd.Amount)
                    .HasPrecision(18, 2);

        modelBuilder.Entity<Transaction>()
                    .Property(t => t.Amount)
                    .HasPrecision(18, 2);     

        modelBuilder.Entity<SavingsGoal>()
                    .HasOne(g => g.User)
                    .WithMany()
                    .HasForeignKey(g => g.UserId)
                    .OnDelete(DeleteBehavior.Cascade);


    }


}
