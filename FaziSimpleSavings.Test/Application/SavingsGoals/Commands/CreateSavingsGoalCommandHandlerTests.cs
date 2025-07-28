using Application.SavingsGoals.Commands.CreateSavingsGoal;
using FluentAssertions;
using Moq;
using Application.Common.Security;
using Application.Features.SavingsGoals.Commands.CreateSavingsGoal;
using FaziSimpleSavings.Test.TestUtilities;

public class CreateSavingsGoalCommandHandlerTests : BaseTest
{
    [Fact]
    public async Task Should_Create_Goal_When_Input_Is_Valid()
    {
        // Arrange
        var user = TestUserFactory.Create("Jane", "Doe", "jane@example.com");
        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var command = new CreateSavingsGoalCommand
        {
            UserId = user.Id,
            Name = "Emergency Fund",
            TargetAmount = 1000m
        };

        var ownershipValidatorMock = new Mock<IOwnershipValidator>();
        var handler = new CreateSavingsGoalCommandHandler(Context, ownershipValidatorMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Should().BeTrue();
        Context.SavingsGoals.Should().ContainSingle(g => g.Name == "Emergency Fund" && g.UserId == user.Id);
    }

    [Fact]
    public async Task Should_Not_Create_Goal_When_Amount_Is_Zero()
    {
        // Arrange
        var user = TestUserFactory.Create();
        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var command = new CreateSavingsGoalCommand
        {
            UserId = user.Id,
            Name = "Invalid Goal",
            TargetAmount = 0
        };

        var validator = new CreateSavingsGoalCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "TargetAmount");
    }
}
