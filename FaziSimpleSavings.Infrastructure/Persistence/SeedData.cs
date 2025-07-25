
using FaziSimpleSavings.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static class SeedData
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var roles = new List<Role>
    {
        new("Admin"),
        new("Customer")
    };
        context.Roles.AddRange(roles);

        var adminUser = new User("Admin", "User", "admin@example.com");
        var customerUser = new User("John", "Doe", "john@example.com");

        context.Users.AddRange(adminUser, customerUser);

        context.UserRoles.AddRange(
            new UserRole { User = adminUser, Role = roles.First(r => r.Name == "Admin") },
            new UserRole { User = customerUser, Role = roles.First(r => r.Name == "Customer") }
        );

        await context.SaveChangesAsync();
    }
}
