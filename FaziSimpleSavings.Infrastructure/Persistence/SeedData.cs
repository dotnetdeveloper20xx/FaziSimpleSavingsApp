using FaziSimpleSavings.Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Users.AnyAsync()) return;

        // Roles
        var roles = new List<Role>
        {
            new("Admin"),
            new("Customer")
        };
        context.Roles.AddRange(roles);

        // Users
        var adminUser = new User("Admin", "User", "admin@example.com");
        var customerUser = new User("John", "Doe", "john@example.com");
        var extraUser = new User("Jane", "Smith", "jane@example.com");

        context.Users.AddRange(adminUser, customerUser, extraUser);

        // UserRoles
        context.UserRoles.AddRange(
            new UserRole { User = adminUser, Role = roles.First(r => r.Name == "Admin") },
            new UserRole { User = customerUser, Role = roles.First(r => r.Name == "Customer") },
            new UserRole { User = extraUser, Role = roles.First(r => r.Name == "Customer") }
        );

        // UserSettings
        context.UserSettings.AddRange(
            new UserSettings(adminUser.Id, "GBP", true),
            new UserSettings(customerUser.Id, "USD", false),
            new UserSettings(extraUser.Id, "EUR", true)
        );

        // Goal Categories
        var adminCategory = new GoalCategory(adminUser.Id, "Emergency", "Emergency fund");
        var customerCategory = new GoalCategory(customerUser.Id, "Vacation", "Travel & leisure");
        var extraCategory = new GoalCategory(extraUser.Id, "Tech", "Gadget savings");

        context.GoalCategories.AddRange(adminCategory, customerCategory, extraCategory);

        await context.SaveChangesAsync(); // Save Users, Roles, Settings, Categories

        // Savings Goals
        var adminGoal = new SavingsGoal("Emergency Fund", 1000m, adminUser.Id);
        var customerGoal1 = new SavingsGoal("Holiday Trip", 800m, customerUser.Id);
        var customerGoal2 = new SavingsGoal("New Car", 3000m, customerUser.Id);
        var extraGoal = new SavingsGoal("New Laptop", 1500m, extraUser.Id);

        context.SavingsGoals.AddRange(adminGoal, customerGoal1, customerGoal2, extraGoal);
        await context.SaveChangesAsync(); // Save goals for FK references

        // Transactions
        context.Transactions.AddRange(
            new Transaction(adminUser.Id, adminGoal.Id, 200m),
            new Transaction(customerUser.Id, customerGoal1.Id, 100m),
            new Transaction(customerUser.Id, customerGoal2.Id, 400m),
            new Transaction(extraUser.Id, extraGoal.Id, 300m)
        );

        // Notifications
        context.Notifications.AddRange(
            new Notification(adminUser.Id, "£200 deposited to Emergency Fund"),
            new Notification(customerUser.Id, "£100 deposited to Holiday Trip"),
            new Notification(customerUser.Id, "£400 deposited to New Car goal"),
            new Notification(extraUser.Id, "€300 deposited to New Laptop")
        );

        // Recurring Deposits
        var rd1 = new RecurringDeposit(adminUser.Id, adminGoal.Id, 50m, "Monthly", DateTime.UtcNow.AddDays(1));
        rd1.ForceSetNextDueDate(DateTime.UtcNow.AddDays(-1)); // overdue

        var rd2 = new RecurringDeposit(customerUser.Id, customerGoal2.Id, 100m, "Weekly", DateTime.UtcNow.AddDays(3));
        var rd3 = new RecurringDeposit(extraUser.Id, extraGoal.Id, 75m, "Monthly", DateTime.UtcNow.AddDays(10));

        context.RecurringDeposits.AddRange(rd1, rd2, rd3);

        await context.SaveChangesAsync(); // Final commit
    }
}
