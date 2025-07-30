
using Microsoft.EntityFrameworkCore;
using FaziSimpleSavings.Application.GroupGoals.Commands.AddMemberToGroupGoal;
using FaziSimpleSavings.Core.Entities;
using Infrastructure.Persistence;

public class AddMemberToGroupGoalCommandHandlerTests
{
    private AppDbContext CreateDb(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Should_Add_Member_When_Valid_Request()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var newMemberId = Guid.NewGuid();

        var goal = new GroupSavingsGoal("Shared Trip", 1000, creatorId);

        using var db = CreateDb(nameof(Should_Add_Member_When_Valid_Request));
        db.GroupSavingsGoals.Add(goal);
        await db.SaveChangesAsync();

        var command = new AddMemberToGroupGoalCommand(goal.Id, newMemberId, creatorId);
        var handler = new AddMemberToGroupGoalCommandHandler(db);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        var member = await db.GroupGoalMembers
            .FirstOrDefaultAsync(m => m.GroupGoalId == goal.Id && m.UserId == newMemberId);
        Assert.NotNull(member);
    }

    [Fact]
    public async Task Should_Throw_If_Requester_Is_Not_Creator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var notCreatorId = Guid.NewGuid();
        var newMemberId = Guid.NewGuid();

        var goal = new GroupSavingsGoal("House Fund", 5000, creatorId);

        using var db = CreateDb(nameof(Should_Throw_If_Requester_Is_Not_Creator));
        db.GroupSavingsGoals.Add(goal);
        await db.SaveChangesAsync();

        var command = new AddMemberToGroupGoalCommand(goal.Id, newMemberId, notCreatorId);
        var handler = new AddMemberToGroupGoalCommandHandler(db);

        // Act + Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_Throw_If_User_Already_A_Member()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var newMemberId = Guid.NewGuid();

        var goal = new GroupSavingsGoal("Travel Goal", 3000, creatorId);
        var member = new GroupGoalMember(goal.Id, newMemberId);
        goal.AddMember(member);

        using var db = CreateDb(nameof(Should_Throw_If_User_Already_A_Member));
        db.GroupSavingsGoals.Add(goal);
        db.GroupGoalMembers.Add(member);
        await db.SaveChangesAsync();

        var command = new AddMemberToGroupGoalCommand(goal.Id, newMemberId, creatorId);
        var handler = new AddMemberToGroupGoalCommandHandler(db);

        // Act + Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_Throw_If_Group_Not_Found()
    {
        // Arrange
        var fakeGoalId = Guid.NewGuid();
        var creatorId = Guid.NewGuid();
        var newMemberId = Guid.NewGuid();

        using var db = CreateDb(nameof(Should_Throw_If_Group_Not_Found));

        var command = new AddMemberToGroupGoalCommand(fakeGoalId, newMemberId, creatorId);
        var handler = new AddMemberToGroupGoalCommandHandler(db);

        // Act + Assert
        await Assert.ThrowsAsync<Exception>(() =>
            handler.Handle(command, CancellationToken.None));
    }
}
