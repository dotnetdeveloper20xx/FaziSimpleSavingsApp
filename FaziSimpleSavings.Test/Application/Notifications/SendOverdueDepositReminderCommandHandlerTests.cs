using Application.Notifications.Commands.SendOverdueDepositReminder;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

using FluentAssertions;

public class SendOverdueDepositReminderCommandHandlerTests
{
    private AppDbContext CreateInMemoryDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Should_Create_Notification_When_Overdue_Deposit_Is_Reported()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var goalName = "Emergency Fund";
        var amount = 50m;

        using var context = CreateInMemoryDbContext(nameof(Should_Create_Notification_When_Overdue_Deposit_Is_Reported));
        var handler = new SendOverdueDepositReminderCommandHandler(context);

        var command = new SendOverdueDepositReminderCommand(userId, goalName, amount);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        var notification = await context.Notifications.FirstOrDefaultAsync();
        notification.Should().NotBeNull();
        notification!.UserId.Should().Be(userId);
        notification.Message.Should().Contain("£50").And.Contain(goalName);
        notification.IsRead.Should().BeFalse();
        notification.Id.Should().Be(result);
    }
}
