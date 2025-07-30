
using Microsoft.EntityFrameworkCore;
using FaziSimpleSavings.Application.GroupGoals.Queries.GetGroupGoalDetails;
using FaziSimpleSavings.Core.Entities;
using Infrastructure.Persistence;

public class GetGroupGoalDetailsQueryHandlerTests
{
    private AppDbContext CreateDb(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Should_Return_Details_If_User_Is_Member()
    {
        var creatorId = Guid.NewGuid();
        var memberId = Guid.NewGuid();

        var user = new User("Test", "User", "test@example.com");
        var creator = new User("Faz", "Ahmed", "faz@example.com");

        var goal = new GroupSavingsGoal("Wedding", 8000, creator.Id);
        var member = new GroupGoalMember(goal.Id, memberId);
        goal.AddMember(member);

        using var db = CreateDb(nameof(Should_Return_Details_If_User_Is_Member));
        db.Users.AddRange(user, creator);
        db.GroupSavingsGoals.Add(goal);
        db.GroupGoalMembers.Add(member);
        await db.SaveChangesAsync();

        var handler = new GetGroupGoalDetailsQueryHandler(db);
        var dto = await handler.Handle(new GetGroupGoalDetailsQuery(goal.Id, memberId), CancellationToken.None);

        Assert.Equal(goal.Name, dto.Name);
        Assert.Equal(creator.Id, goal.CreatedByUserId);
        Assert.Equal(creator.FirstName + " " + creator.LastName, dto.CreatedByName);
        Assert.Single(dto.Members);
    }
}
