using Application.SavingsGoals.Queries.GetGoalProgress;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using FaziSimpleSavings.Core.Entities;

public class GetGoalProgressQueryHandlerTests
{
    private AppDbContext CreateInMemoryDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Should_Return_Progress_For_Existing_Goals()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var goal1 = new SavingsGoal("Holiday", 1000m, userId);
        goal1.AddDeposit(250m);

        var goal2 = new SavingsGoal("Emergency", 500m, userId);
        goal2.AddDeposit(500m);

        using var context = CreateInMemoryDbContext(nameof(Should_Return_Progress_For_Existing_Goals));
        context.SavingsGoals.AddRange(goal1, goal2);
        await context.SaveChangesAsync();

        var handler = new GetGoalProgressQueryHandler(context);

        // Act
        var result = await handler.Handle(new GetGoalProgressQuery(userId), CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);

        var holiday = result.Find(g => g.Name == "Holiday");
        holiday.Should().NotBeNull();
        holiday.CurrentAmount.Should().Be(250m);
        holiday.TargetAmount.Should().Be(1000m);
        holiday.ProgressPercentage.Should().Be(25);
        holiday.IsGoalAchieved.Should().BeFalse();

        var emergency = result.Find(g => g.Name == "Emergency");
        emergency.Should().NotBeNull();
        emergency.ProgressPercentage.Should().Be(100);
        emergency.IsGoalAchieved.Should().BeTrue();
    }

    [Fact]
    public async Task Should_Return_Empty_List_When_No_Goals_Exist()
    {
        // Arrange
        var userId = Guid.NewGuid();

        using var context = CreateInMemoryDbContext(nameof(Should_Return_Empty_List_When_No_Goals_Exist));
        var handler = new GetGoalProgressQueryHandler(context);

        // Act
        var result = await handler.Handle(new GetGoalProgressQuery(userId), CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
}
