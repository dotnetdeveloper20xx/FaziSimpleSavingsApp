
using Microsoft.EntityFrameworkCore;
using FaziSimpleSavings.Application.GroupGoals.Commands.ContributeToGroupGoal;
using FaziSimpleSavings.Core.Entities;
using Infrastructure.Persistence;

public class ContributeToGroupGoalCommandHandlerTests
{
    private AppDbContext CreateDb(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Should_Contribute_To_Group_And_Record_Transaction()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var goal = new GroupSavingsGoal("Trip", 1000, userId);
        var member = new GroupGoalMember(goal.Id, userId);
        goal.AddMember(member);

        using var db = CreateDb(nameof(Should_Contribute_To_Group_And_Record_Transaction));
        db.GroupSavingsGoals.Add(goal);
        db.GroupGoalMembers.Add(member);
        await db.SaveChangesAsync();

        var handler = new ContributeToGroupGoalCommandHandler(db);
        var command = new ContributeToGroupGoalCommand(goal.Id, 300, userId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedGoal = await db.GroupSavingsGoals.FindAsync(goal.Id);
        var updatedMember = await db.GroupGoalMembers.FindAsync(member.Id);
        var tx = await db.GroupTransactions.FirstOrDefaultAsync(t => t.GroupGoalId == goal.Id && t.UserId == userId);

        Assert.True(result);
        Assert.Equal(300, updatedGoal!.TotalSaved);
        Assert.Equal(300, updatedMember!.ContributedAmount);
        Assert.NotNull(tx);
        Assert.Equal(300, tx!.Amount);
    }
}
