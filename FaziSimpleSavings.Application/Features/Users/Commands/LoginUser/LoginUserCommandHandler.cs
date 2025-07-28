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

        //Find the first user whose email matches the one from the request. While retrieving the user, also include their roles.
        var user = await _context.Users
     .Include(u => u.UserRoles)
         .ThenInclude(ur => ur.Role)
     .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user == null || request.Password != "Secure123!")
            throw new UnauthorizedAccessException("Invalid credentials.");

        // For demo/testing: password is not encrypted
        if (request.Password != "Secure123!") // hardcoded check
            throw new UnauthorizedAccessException("Invalid credentials.");

        return _tokenGenerator.GenerateToken(user);
    }
}
