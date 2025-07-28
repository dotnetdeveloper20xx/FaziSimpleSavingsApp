using Application.Common.Security;
using Application.Interfaces;
using Application.Transactions.Commands.CreateManualTransaction;
using FaziSimpleSavings.Application.Notifications.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class CreateManualTransactionCommandHandler : IRequestHandler<CreateManualTransactionCommand, bool>
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

    public async Task<bool> Handle(CreateManualTransactionCommand request, CancellationToken cancellationToken)
    {
        if (!await _ownershipValidator.UserOwnsGoal(request.UserId, request.GoalId))
            return false;

        var goal = await _context.SavingsGoals
            .FirstOrDefaultAsync(g => g.Id == request.GoalId, cancellationToken);
        if (goal == null)
            return false;

        goal.AddDeposit(request.Amount);

        var transaction = new Transaction(request.UserId, request.GoalId, request.Amount);
        _context.Transactions.Add(transaction);

        await _context.SaveChangesAsync(cancellationToken);

        // ✅ Send notification after successful save
        var message = $"You deposited £{request.Amount} to your goal \"{goal.Name}\".";
        await _mediator.Send(new CreateNotificationCommand(request.UserId, message));

        return true;
    }
}
