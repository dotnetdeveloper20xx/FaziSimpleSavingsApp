using MediatR;

namespace Application.Features.Users.Commands.LoginUser;

public class LoginUserCommand : IRequest<string> // JWT token on success
{
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}
