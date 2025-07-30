using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FaziSimpleSavings.Application.GroupGoals.Queries.GetAvailableUsersForGroupGoal;
using FaziSimpleSavings.Core.Entities;
using Infrastructure.Persistence;

public class GetAvailableUsersForGroupGoalQueryHandlerTests
{
    private AppDbContext CreateDb(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new AppDbContext(options);
    }

    private User CreateUser(Guid id, string first, string last, string email)
    {
        var user = new User(first, last, email);
        typeof(User).GetProperty("Id")!.SetValue(user, id);
        return user;
    }

    [Fact]
    public async Task Should_Return_Users_Not_In_Group_When_Creator_Requests()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var goal = new GroupSavingsGoal("Group Holiday", 1000, creatorId);

        var user1 = CreateUser(Guid.NewGuid(), "Emma", "Stone", "emma@example.com");
        var user2 = CreateUser(Guid.NewGuid(), "Jake", "Smith", "jake@example.com");
        var user3Id = Guid.NewGuid(); // Already a member
        var user3 = CreateUser(user3Id, "Amy", "Wong", "amy@example.com");

        var member = new GroupGoalMember(goal.Id, user3Id);
        goal.AddMember(member);

        using var db = CreateDb(nameof(Should_Return_Users_Not_In_Group_When_Creator_Requests));
        db.Users.AddRange(user1, user2, user3);
        db.GroupSavingsGoals.Add(goal);
        db.GroupGoalMembers.Add(member);
        await db.SaveChangesAsync();

        var handler = new GetAvailableUsersForGroupGoalQueryHandler(db);
        var query = new GetAvailableUsersForGroupGoalQuery(goal.Id, creatorId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count);
        var returnedEmails = result.Select(r => r.Email).ToList();
        Assert.Contains("emma@example.com", returnedEmails);
        Assert.Contains("jake@example.com", returnedEmails);
        Assert.DoesNotContain("amy@example.com", returnedEmails);
    }

    [Fact]
    public async Task Should_Throw_If_Requesting_User_Is_Not_Creator()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var notCreatorId = Guid.NewGuid();
        var goal = new GroupSavingsGoal("Invalid Access Goal", 1000, creatorId);

        using var db = CreateDb(nameof(Should_Throw_If_Requesting_User_Is_Not_Creator));
        db.GroupSavingsGoals.Add(goal);
        await db.SaveChangesAsync();

        var handler = new GetAvailableUsersForGroupGoalQueryHandler(db);
        var query = new GetAvailableUsersForGroupGoalQuery(goal.Id, notCreatorId);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Should_Throw_If_GroupGoal_Not_Found()
    {
        // Arrange
        var query = new GetAvailableUsersForGroupGoalQuery(Guid.NewGuid(), Guid.NewGuid());

        using var db = CreateDb(nameof(Should_Throw_If_GroupGoal_Not_Found));
        var handler = new GetAvailableUsersForGroupGoalQueryHandler(db);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            handler.Handle(query, CancellationToken.None));
    }
}
