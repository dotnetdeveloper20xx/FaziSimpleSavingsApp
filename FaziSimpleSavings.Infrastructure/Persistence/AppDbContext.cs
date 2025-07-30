using Application.Interfaces;
using FaziSimpleSavings.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<SavingsGoal> SavingsGoals { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<RecurringDeposit> RecurringDeposits { get; set; }
    public DbSet<GoalCategory> GoalCategories { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }
    public DbSet<GroupSavingsGoal> GroupSavingsGoals { get; set; } = null!;
    public DbSet<GroupGoalMember> GroupGoalMembers { get; set; } = null!;
    public DbSet<GroupTransaction> GroupTransactions { get; set; } = null!;


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

        modelBuilder.Entity<GroupSavingsGoal>()
                    .HasMany(g => g.Members)
                    .WithOne(m => m.GroupGoal)
                    .HasForeignKey(m => m.GroupGoalId)
                    .OnDelete(DeleteBehavior.Restrict); //  Cascade with Restrict or NoAction



    }


}
