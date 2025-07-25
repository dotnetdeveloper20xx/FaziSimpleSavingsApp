using Application.Interfaces;
using FaziSimpleSavings.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FaziSimpleSavings.Application.Features.Users.Commands.RegisterUser;

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
        if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            throw new InvalidOperationException("Email already registered.");

        var user = new User(request.FirstName, request.LastName, request.Email);

        // NOTE: In real apps you'd hash the password here!

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return _tokenGenerator.GenerateToken(user);
    }
}
