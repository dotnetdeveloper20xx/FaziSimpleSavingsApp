
using Application.Common.Security;
using Application.Features.Transactions.Commands.CreateManualTransaction;
using Application.Interfaces;
using Application.Transactions.Commands.CreateManualTransaction;
using FaziSimpleSavings.Application.Common.Exceptions;
using FaziSimpleSavings.Application.Notifications.Commands;
using FaziSimpleSavings.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class CreateManualTransactionCommandHandlerTests
{
    private readonly Mock<IAppDbContext> _dbContextMock = new();
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly Mock<IOwnershipValidator> _ownershipValidatorMock = new();

    private CreateManualTransactionCommandHandler CreateHandler(SavingsGoal? goal = null)
    {
        var goals = new[] { goal }.Where(g => g != null).AsQueryable();

        var savingsGoalsMock = new Mock<DbSet<SavingsGoal>>();
        savingsGoalsMock.As<IQueryable<SavingsGoal>>().Setup(m => m.Provider).Returns(goals.Provider);
        savingsGoalsMock.As<IQueryable<SavingsGoal>>().Setup(m => m.Expression).Returns(goals.Expression);
        savingsGoalsMock.As<IQueryable<SavingsGoal>>().Setup(m => m.ElementType).Returns(goals.ElementType);
        savingsGoalsMock.As<IQueryable<SavingsGoal>>().Setup(m => m.GetEnumerator()).Returns(goals.GetEnumerator());

        _dbContextMock.Setup(c => c.SavingsGoals).Returns(savingsGoalsMock.Object);
        _dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        return new CreateManualTransactionCommandHandler(
            _dbContextMock.Object,
            _ownershipValidatorMock.Object,
            _mediatorMock.Object
        );
    }

    [Fact]
    public async Task Should_Create_Transaction_And_Send_Notifications()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var goal = new SavingsGoal("Test Goal", 100, userId);
        var handler = CreateHandler(goal);

        _ownershipValidatorMock.Setup(v => v.UserOwnsGoal(userId, goal.Id)).ReturnsAsync(true);

        var request = new CreateManualTransactionCommand(userId, goal.Id, 50);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(50, goal.CurrentAmount);
        _mediatorMock.Verify(m => m.Send(It.IsAny<CreateNotificationCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_Throw_When_Goal_Is_Already_Achieved()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var goal = new SavingsGoal("Test Goal", 100, userId);
        goal.AddDeposit(100); // Fully funded

        var handler = CreateHandler(goal);
        _ownershipValidatorMock.Setup(v => v.UserOwnsGoal(userId, goal.Id)).ReturnsAsync(true);

        var request = new CreateManualTransactionCommand(userId, goal.Id, 10);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Should_Throw_When_User_Does_Not_Own_Goal()
    {
        var userId = Guid.NewGuid();
        var goal = new SavingsGoal("Test Goal", 100, userId);
        var handler = CreateHandler(goal);

        _ownershipValidatorMock.Setup(v => v.UserOwnsGoal(userId, goal.Id)).ReturnsAsync(false);

        var request = new CreateManualTransactionCommand(userId, goal.Id, 10);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(request, CancellationToken.None));
    }
}
