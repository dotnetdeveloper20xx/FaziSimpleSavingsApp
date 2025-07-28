using Application.Notifications.Commands.MarkNotificationAsRead;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Common.Security;
using Moq;

using FaziSimpleSavings.Core.Entities;

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
    public async Task Should_Mark_Notification_As_Read_When_It_Exists_And_User_Owns_It()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var notification = new Notification(userId, "Test notification");

        using var context = CreateInMemoryDbContext(nameof(Should_Mark_Notification_As_Read_When_It_Exists_And_User_Owns_It));
        context.Notifications.Add(notification);
        await context.SaveChangesAsync();

        var ownershipValidatorMock = new Mock<IOwnershipValidator>();
        ownershipValidatorMock
            .Setup(v => v.UserOwnsNotification(userId, notification.Id))
            .ReturnsAsync(true);

        var handler = new MarkNotificationAsReadCommandHandler(context, ownershipValidatorMock.Object);

        // Act
        var result = await handler.Handle(new MarkNotificationAsReadCommand(notification.Id, userId), CancellationToken.None);

        // Assert
        Assert.True(result);
        Assert.True(notification.IsRead);
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

        var ownershipValidatorMock = new Mock<IOwnershipValidator>();
        ownershipValidatorMock
            .Setup(v => v.UserOwnsNotification(userId, notification.Id))
            .ReturnsAsync(false);

        var handler = new MarkNotificationAsReadCommandHandler(context, ownershipValidatorMock.Object);

        // Act
        var result = await handler.Handle(new MarkNotificationAsReadCommand(notification.Id, userId), CancellationToken.None);

        // Assert
        Assert.False(result);
        Assert.False(notification.IsRead);
    }

    [Fact]
    public async Task Should_Return_False_If_Notification_Not_Found_After_Ownership_Check()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var notificationId = Guid.NewGuid(); // Not stored in DB

        using var context = CreateInMemoryDbContext(nameof(Should_Return_False_If_Notification_Not_Found_After_Ownership_Check));

        var ownershipValidatorMock = new Mock<IOwnershipValidator>();
        ownershipValidatorMock
            .Setup(v => v.UserOwnsNotification(userId, notificationId))
            .ReturnsAsync(true); // Ownership says yes, but record not in DB

        var handler = new MarkNotificationAsReadCommandHandler(context, ownershipValidatorMock.Object);

        // Act
        var result = await handler.Handle(new MarkNotificationAsReadCommand(notificationId, userId), CancellationToken.None);

        // Assert
        Assert.False(result);
    }
}
