using Application.Transactions.Queries.GetTransactionsByGoal;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


public class GetTransactionsByGoalQueryHandlerTests
{
    private AppDbContext CreateInMemoryDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Should_Return_Transactions_For_Specific_User_And_Goal()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var otherGoalId = Guid.NewGuid();

        var transaction1 = new Transaction(userId, goalId, 100m);
        var transaction2 = new Transaction(userId, goalId, 50m);
        var transactionOther = new Transaction(userId, otherGoalId, 30m); // should not be included

        using var context = CreateInMemoryDbContext(nameof(Should_Return_Transactions_For_Specific_User_And_Goal));
        context.Transactions.AddRange(transaction1, transaction2, transactionOther);
        await context.SaveChangesAsync();

        var handler = new GetTransactionsByGoalQueryHandler(context);

        // Act
        var result = await handler.Handle(new GetTransactionsByGoalQuery(userId, goalId), CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, t => t.Amount == 100m);
        Assert.Contains(result, t => t.Amount == 50m);
    }

    [Fact]
    public async Task Should_Return_Empty_List_When_No_Transactions_Match()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var goalId = Guid.NewGuid();

        using var context = CreateInMemoryDbContext(nameof(Should_Return_Empty_List_When_No_Transactions_Match));
        // No transactions added

        var handler = new GetTransactionsByGoalQueryHandler(context);

        // Act
        var result = await handler.Handle(new GetTransactionsByGoalQuery(userId, goalId), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
