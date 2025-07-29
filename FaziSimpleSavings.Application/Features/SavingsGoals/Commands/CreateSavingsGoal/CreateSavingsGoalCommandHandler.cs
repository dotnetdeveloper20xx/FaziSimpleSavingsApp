using Application.Common.Security;
using Application.Features.SavingsGoals.Commands.CreateSavingsGoal;
using Application.Interfaces;
using FaziSimpleSavings.Application.Notifications.Commands;
using FaziSimpleSavings.Core.Entities;
using MediatR;

namespace Application.SavingsGoals.Commands.CreateSavingsGoal;

public class CreateSavingsGoalCommandHandler : IRequestHandler<CreateSavingsGoalCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly IOwnershipValidator _ownershipValidator;
    private readonly IMediator _mediator;

    public CreateSavingsGoalCommandHandler(
        IAppDbContext context,
        IOwnershipValidator ownershipValidator,
        IMediator mediator)
    {
        _context = context;
        _ownershipValidator = ownershipValidator;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateSavingsGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = new SavingsGoal(request.Name, request.TargetAmount, request.UserId);

        _context.SavingsGoals.Add(goal);
        await _context.SaveChangesAsync(cancellationToken);

        // Create notification
        var message = $"Your new savings goal \"{goal.Name}\" with a target of £{goal.TargetAmount} has been created.";
        await _mediator.Send(new CreateNotificationCommand(request.UserId, message), cancellationToken);

        return goal.Id;
    }
}
