using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FaziSimpleSavings.Application.GroupGoals.Queries.GetGroupGoalTransactions;
using FaziSimpleSavings.Core.Entities;
using Infrastructure.Persistence;

public class GetGroupGoalTransactionsQueryHandlerTests
{
    private AppDbContext CreateDb(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new AppDbContext(options);
    }

    private User CreateUser(Guid id, string firstName, string lastName, string email)
    {
        var user = new User(firstName, lastName, email);
        typeof(User).GetProperty("Id")!.SetValue(user, id);
        return user;
    }

    [Fact]
    public async Task Should_Return_Transactions_When_User_Is_Member()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var creatorId = Guid.NewGuid();

        var goal = new GroupSavingsGoal("Group Trip", 1000, creatorId);
        var member = new GroupGoalMember(goal.Id, userId);
        goal.AddMember(member);

        var tx1 = new GroupTransaction(goal.Id, userId, 100);
        var tx2 = new GroupTransaction(goal.Id, userId, 200);

        var user = CreateUser(userId, "Jane", "Doe", "jane@example.com");

        using var db = CreateDb(nameof(Should_Return_Transactions_When_User_Is_Member));
        db.Users.Add(user);
        db.GroupSavingsGoals.Add(goal);
        db.GroupGoalMembers.Add(member);
        db.GroupTransactions.AddRange(tx1, tx2);
        await db.SaveChangesAsync();

        var handler = new GetGroupGoalTransactionsQueryHandler(db);

        // Act
        var result = await handler.Handle(new GetGroupGoalTransactionsQuery(goal.Id, userId), CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, t => t.Amount == 100);
        Assert.Contains(result, t => t.Amount == 200);
        Assert.All(result, t => Assert.Equal("Jane Doe", t.UserFullName));
    }

    [Fact]
    public async Task Should_Throw_If_User_Not_A_Member()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var nonMemberId = Guid.NewGuid();

        var goal = new GroupSavingsGoal("Group House", 5000, memberId);
        var member = new GroupGoalMember(goal.Id, memberId);
        goal.AddMember(member);

        using var db = CreateDb(nameof(Should_Throw_If_User_Not_A_Member));
        db.GroupSavingsGoals.Add(goal);
        db.GroupGoalMembers.Add(member);
        await db.SaveChangesAsync();

        var handler = new GetGroupGoalTransactionsQueryHandler(db);
        var query = new GetGroupGoalTransactionsQuery(goal.Id, nonMemberId);

        // Act + Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Should_Return_Empty_List_If_No_Transactions()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var goal = new GroupSavingsGoal("No Spends", 1000, userId);
        var member = new GroupGoalMember(goal.Id, userId);
        goal.AddMember(member);

        var user = CreateUser(userId, "No", "Spend", "none@example.com");

        using var db = CreateDb(nameof(Should_Return_Empty_List_If_No_Transactions));
        db.Users.Add(user);
        db.GroupSavingsGoals.Add(goal);
        db.GroupGoalMembers.Add(member);
        await db.SaveChangesAsync();

        var handler = new GetGroupGoalTransactionsQueryHandler(db);
        var result = await handler.Handle(new GetGroupGoalTransactionsQuery(goal.Id, userId), CancellationToken.None);

        // Assert
        Assert.Empty(result);
    }
}
