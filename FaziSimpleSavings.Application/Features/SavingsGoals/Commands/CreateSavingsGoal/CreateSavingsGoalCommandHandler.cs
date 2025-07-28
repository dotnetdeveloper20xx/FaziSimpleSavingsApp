
using Application.Common.Security;
using Application.Features.SavingsGoals.Commands.CreateSavingsGoal;
using Application.Interfaces;
using FaziSimpleSavings.Core.Entities;
using MediatR;

namespace Application.SavingsGoals.Commands.CreateSavingsGoal;

public class CreateSavingsGoalCommandHandler : IRequestHandler<CreateSavingsGoalCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly IOwnershipValidator _ownershipValidator;

    public CreateSavingsGoalCommandHandler(IAppDbContext context, IOwnershipValidator ownershipValidator)
    {
        _context = context;
        _ownershipValidator = ownershipValidator;
    }

    public async Task<Guid> Handle(CreateSavingsGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = new SavingsGoal(request.Name, request.TargetAmount, request.UserId);

        _context.SavingsGoals.Add(goal);
        await _context.SaveChangesAsync(cancellationToken);

        return goal.Id;
    }
}

