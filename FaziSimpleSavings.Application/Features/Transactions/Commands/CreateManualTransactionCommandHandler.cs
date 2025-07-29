using Application.Common.Security;
using Application.Interfaces;
using FaziSimpleSavings.Application.Common.Exceptions;
using FaziSimpleSavings.Application.Notifications.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Transactions.Commands.CreateManualTransaction;


public class CreateManualTransactionCommandHandler : IRequestHandler<CreateManualTransactionCommand, Unit>
{
    private readonly IAppDbContext _context;
    private readonly IOwnershipValidator _ownershipValidator;
    private readonly IMediator _mediator;

    public CreateManualTransactionCommandHandler(
        IAppDbContext context,
        IOwnershipValidator ownershipValidator,
        IMediator mediator)
    {
        _context = context;
        _ownershipValidator = ownershipValidator;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(CreateManualTransactionCommand request, CancellationToken cancellationToken)
    {
        // Ownership check
        var ownsGoal = await _ownershipValidator.UserOwnsGoal(request.UserId, request.GoalId);
        if (!ownsGoal)
            throw new NotFoundException("SavingsGoal", request.GoalId);

        var goal = await _context.SavingsGoals
            .FirstOrDefaultAsync(g => g.Id == request.GoalId, cancellationToken);

        if (goal == null)
            throw new NotFoundException("SavingsGoal", request.GoalId);

        goal.AddDeposit(request.Amount);

        var transaction = new Transaction(request.UserId, request.GoalId, request.Amount);
        _context.Transactions.Add(transaction);

        await _context.SaveChangesAsync(cancellationToken);

        // Create notification
        var message = $"£{request.Amount} was manually deposited into your goal \"{goal.Name}\".";
        await _mediator.Send(new CreateNotificationCommand(request.UserId, message), cancellationToken);

        return Unit.Value;
    }
}

