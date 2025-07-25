using Application.Features.SavingsGoals.Queries.GetUserGoals;
using FaziSimpleSavings.Core.Entities;
using FaziSimpleSavings.Test.TestUtilities;
using FluentAssertions;

namespace FaziSimpleSavings.Test.Application.SavingsGoals.Queries;

public class GetUserGoalsQueryHandlerTests : BaseTest
{
    [Fact]
    public async Task Should_Return_Goals_For_User()
    {
        // Arrange
        var user = TestUserFactory.Create("Alice", "Smith", "alice@example.com");
        Context.Users.Add(user);

        var goals = new List<SavingsGoal>
        {
            new("Emergency Fund", 2000m, user.Id),
            new("Vacation Fund", 1000m, user.Id)
        };

        Context.SavingsGoals.AddRange(goals);
        await Context.SaveChangesAsync();

        var query = new GetUserGoalsQuery(user.Id);
        var handler = new GetUserGoalsQueryHandler(Context);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(g => g.Name == "Emergency Fund");
        result.Should().Contain(g => g.Name == "Vacation Fund");
    }

    [Fact]
    public async Task Should_Return_Empty_List_When_User_Has_No_Goals()
    {
        // Arrange
        var user = TestUserFactory.Create("Bob", "Empty", "bob@example.com");
        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var query = new GetUserGoalsQuery(user.Id);
        var handler = new GetUserGoalsQueryHandler(Context);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().BeEmpty();
    }
}
