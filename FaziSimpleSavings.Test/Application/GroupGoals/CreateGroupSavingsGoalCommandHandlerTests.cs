
using Microsoft.EntityFrameworkCore;
using FaziSimpleSavings.Application.Features.GroupGoals.Commands;
using Infrastructure.Persistence;

public class CreateGroupSavingsGoalCommandHandlerTests
{
    private AppDbContext CreateInMemoryDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Should_Create_GroupGoal_And_Add_Creator_As_Member()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new CreateGroupSavingsGoalCommand
        {
            Name = "Shared Wedding Fund",
            TargetAmount = 10000,
            UserId = userId
        };

        using var context = CreateInMemoryDbContext(nameof(Should_Create_GroupGoal_And_Add_Creator_As_Member));
        var handler = new CreateGroupSavingsGoalCommandHandler(context);

        // Act
        var goalId = await handler.Handle(command, CancellationToken.None);

        // Assert
        var groupGoal = await context.GroupSavingsGoals.FindAsync(goalId);
        Assert.NotNull(groupGoal);
        Assert.Equal(command.Name, groupGoal!.Name);
        Assert.Equal(command.TargetAmount, groupGoal.TargetAmount);
        Assert.Equal(userId, groupGoal.CreatedByUserId);

        var member = await context.GroupGoalMembers
            .FirstOrDefaultAsync(m => m.GroupGoalId == goalId && m.UserId == userId);
        Assert.NotNull(member);
    }
}
