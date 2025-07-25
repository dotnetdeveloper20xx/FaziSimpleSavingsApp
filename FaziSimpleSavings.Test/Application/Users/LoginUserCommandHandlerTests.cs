using Application.Features.Users.Commands.LoginUser;
using FluentAssertions;
using FaziSimpleSavings.Test.TestUtilities;
using Moq;
using Application.Interfaces;

namespace FaziSimpleSavings.Test.Application.Users;

public class LoginUserCommandHandlerTests : BaseTest
{
    private readonly Mock<IJwtTokenGenerator> _tokenGeneratorMock = new();

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var user = TestUserFactory.Create("Jane", "Doe", "jane@example.com");
        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        _tokenGeneratorMock.Setup(x => x.GenerateToken(user))
            .Returns("mock-jwt-token");

        var handler = new LoginUserCommandHandler(Context, _tokenGeneratorMock.Object);
        var command = new LoginUserCommand
        {
            Email = "jane@example.com",
            Password = "password"
        };

        // Act
        var token = await handler.Handle(command, default);

        // Assert
        token.Should().Be("mock-jwt-token");
    }
}
