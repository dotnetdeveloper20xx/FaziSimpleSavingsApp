using Application.Notifications.Commands.MarkNotificationAsRead;
using FaziSimpleSavings.Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


public class MarkNotificationAsReadCommandHandlerTests
{
    private AppDbContext CreateInMemoryDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Should_Mark_Notification_As_Read_When_It_Exists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var notification = new Notification(userId, "Test notification");

        using var context = CreateInMemoryDbContext(nameof(Should_Mark_Notification_As_Read_When_It_Exists));
        context.Notifications.Add(notification);
        await context.SaveChangesAsync();

        var handler = new MarkNotificationAsReadCommandHandler(context);

        // Act
        var result = await handler.Handle(new MarkNotificationAsReadCommand(notification.Id, userId), CancellationToken.None);

        // Assert
        Assert.True(result);
        Assert.True(notification.IsRead);
    }

    [Fact]
    public async Task Should_Return_False_If_Notification_Does_Not_Exist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var nonExistentNotificationId = Guid.NewGuid();

        using var context = CreateInMemoryDbContext(nameof(Should_Return_False_If_Notification_Does_Not_Exist));

        var handler = new MarkNotificationAsReadCommandHandler(context);

        // Act
        var result = await handler.Handle(new MarkNotificationAsReadCommand(nonExistentNotificationId, userId), CancellationToken.None);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Should_Return_False_If_User_Does_Not_Own_Notification()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var otherUserId = Guid.NewGuid();
        var notification = new Notification(otherUserId, "Other user's notification");

        using var context = CreateInMemoryDbContext(nameof(Should_Return_False_If_User_Does_Not_Own_Notification));
        context.Notifications.Add(notification);
        await context.SaveChangesAsync();

        var handler = new MarkNotificationAsReadCommandHandler(context);

        // Act
        var result = await handler.Handle(new MarkNotificationAsReadCommand(notification.Id, userId), CancellationToken.None);

        // Assert
        Assert.False(result);
        Assert.False(notification.IsRead);
    }
}
