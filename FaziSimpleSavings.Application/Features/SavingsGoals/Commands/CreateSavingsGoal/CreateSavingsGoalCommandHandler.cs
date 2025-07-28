
using Application.Common.Security;
using Application.Features.SavingsGoals.Commands.CreateSavingsGoal;
using Application.Interfaces;

using FaziSimpleSavings.Core.Entities;
using MediatR;

namespace Application.SavingsGoals.Commands.CreateSavingsGoal;

public class CreateSavingsGoalCommandHandler : IRequestHandler<CreateSavingsGoalCommand, bool>
{
    private readonly IAppDbContext _context;
    private readonly IOwnershipValidator _ownershipValidator;

    public CreateSavingsGoalCommandHandler(IAppDbContext context, IOwnershipValidator ownershipValidator)
    {
        _context = context;
        _ownershipValidator = ownershipValidator;
    }

    public async Task<bool> Handle(CreateSavingsGoalCommand request, CancellationToken cancellationToken)
    {
        // OPTIONAL: If you support creating goal under a GoalCategory, validate ownership:
        // if (request.GoalCategoryId.HasValue)
        // {
        //     var ownsCategory = await _ownershipValidator.UserOwnsGoalCategory(request.UserId, request.GoalCategoryId.Value);
        //     if (!ownsCategory) return false;
        // }

        var goal = new SavingsGoal(request.Name, request.TargetAmount, request.UserId);

        _context.SavingsGoals.Add(goal);
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}
