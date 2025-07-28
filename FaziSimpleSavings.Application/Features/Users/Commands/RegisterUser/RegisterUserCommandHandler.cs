
using Application.Interfaces;
using FaziSimpleSavings.Application.Features.Users.Commands.RegisterUser;
using FaziSimpleSavings.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly IAppDbContext _context;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public RegisterUserCommandHandler(IAppDbContext context, IJwtTokenGenerator tokenGenerator)
    {
        _context = context;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Check for duplicate email
        if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            throw new InvalidOperationException("Email already registered.");

        // Create the user
        var user = new User(request.FirstName, request.LastName, request.Email);
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        // Find the "Customer" role
        var customerRole = await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == "Customer", cancellationToken);

        if (customerRole == null)
            throw new InvalidOperationException("Default role 'Customer' not found. Please seed roles.");

        // Link user to the role
        var userRole = new UserRole(user.Id, customerRole.Id);
        _context.UserRoles.Add(userRole);
        await _context.SaveChangesAsync(cancellationToken);

        // Generate JWT
        return _tokenGenerator.GenerateToken(user);
    }
}
