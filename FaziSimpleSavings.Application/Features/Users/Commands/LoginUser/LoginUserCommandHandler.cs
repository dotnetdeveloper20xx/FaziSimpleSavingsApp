using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IAppDbContext _context;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginUserCommandHandler(IAppDbContext context, IJwtTokenGenerator tokenGenerator)
    {
        _context = context;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials.");

        // For demo/testing: password is not encrypted
        if (request.Password != "password") // hardcoded check
            throw new UnauthorizedAccessException("Invalid credentials.");

        return _tokenGenerator.GenerateToken(user);
    }
}
