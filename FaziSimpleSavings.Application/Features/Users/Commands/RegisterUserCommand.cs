using MediatR;

namespace Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<string> // Returns JWT token
{
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}
