using Application.Interfaces;
using FaziSimpleSavings.Core.Entities;
using MediatR;

namespace Application.Features.SavingsGoals.Commands.CreateSavingsGoal;

public class CreateSavingsGoalCommandHandler : IRequestHandler<CreateSavingsGoalCommand, bool>
{
    private readonly IAppDbContext _context;

    public CreateSavingsGoalCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CreateSavingsGoalCommand request, CancellationToken cancellationToken)
    {       
        var goal = new SavingsGoal(request.Name, request.TargetAmount, request.UserId);

        _context.SavingsGoals.Add(goal);

        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}
