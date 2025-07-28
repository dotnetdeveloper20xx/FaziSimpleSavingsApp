using Application.SavingsGoals.Commands.CreateSavingsGoal;
using Application.Common.Security;
using FaziSimpleSavings.Test.TestUtilities;
using FluentAssertions;
using FluentValidation;
using Xunit;
using Moq;
using Application.Features.SavingsGoals.Commands.CreateSavingsGoal;

namespace Application.UnitTests.SavingsGoals.Commands;

public class CreateSavingsGoalCommandHandlerTests : BaseTest
{
    [Fact]
    public async Task Should_Create_Goal_When_Input_Is_Valid()
    {
        // Arrange
        var user = TestUserFactory.Create("Jane", "Doe", "jane@example.com");
        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var command = new CreateSavingsGoalCommand(user.Id, "Emergency Fund", 1000m);

        var ownershipValidatorMock = new Mock<IOwnershipValidator>();
        var handler = new CreateSavingsGoalCommandHandler(Context, ownershipValidatorMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Should().NotBeEmpty(); // Guid should not be Guid.Empty
        Context.SavingsGoals.Should().ContainSingle(g => g.Name == "Emergency Fund" && g.UserId == user.Id);
    }

    [Fact]
    public void Should_Fail_Validation_When_TargetAmount_Is_Zero()
    {
        // Arrange
        var command = new CreateSavingsGoalCommand(Guid.NewGuid(), "Invalid Goal", 0);

        var validator = new CreateSavingsGoalCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "TargetAmount");
    }
}

