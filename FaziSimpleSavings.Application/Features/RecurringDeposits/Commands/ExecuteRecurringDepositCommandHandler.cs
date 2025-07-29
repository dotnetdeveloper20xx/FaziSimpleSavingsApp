using Application.Common.Security;
using Application.Interfaces;
using FaziSimpleSavings.Application.Notifications.Commands;
using FaziSimpleSavings.Application.RecurringDeposits.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RecurringDeposits.Commands.ExecuteRecurringDeposit;

public class ExecuteRecurringDepositCommandHandler : IRequestHandler<ExecuteRecurringDepositCommand, bool>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IOwnershipValidator _ownershipValidator;

    public ExecuteRecurringDepositCommandHandler(
        IAppDbContext context,
        IMediator mediator,
        IOwnershipValidator ownershipValidator)
    {
        _context = context;
        _mediator = mediator;
        _ownershipValidator = ownershipValidator;
    }

    public async Task<bool> Handle(ExecuteRecurringDepositCommand request, CancellationToken cancellationToken)
    {
        var recurring = await _context.RecurringDeposits
            .FirstOrDefaultAsync(x => x.Id == request.RecurringDepositId, cancellationToken);
        if (recurring == null)
            return false;

        var ownsGoal = await _ownershipValidator.UserOwnsGoal(recurring.UserId, recurring.GoalId);
        if (!ownsGoal)
            return false;

        var goal = await _context.SavingsGoals
            .FirstOrDefaultAsync(x => x.Id == recurring.GoalId, cancellationToken);
        if (goal == null)
            return false;

        goal.AddDeposit(recurring.Amount);

        var transaction = new Transaction(recurring.UserId, recurring.GoalId, recurring.Amount);
        _context.Transactions.Add(transaction);

        recurring.UpdateNextDueDate();
        await _context.SaveChangesAsync(cancellationToken);

        // Send notification about recurring deposit
        var message = $"£{recurring.Amount} was deposited automatically into your goal \"{goal.Name}\".";
        await _mediator.Send(new CreateNotificationCommand(recurring.UserId, message), cancellationToken);

        // Optional: Notify if goal is now complete
        if (goal.IsGoalAchieved())
        {
            var congratsMsg = $"🎉 Congratulations! Your goal \"{goal.Name}\" has been fully achieved!";
            await _mediator.Send(new CreateNotificationCommand(recurring.UserId, congratsMsg), cancellationToken);
        }

        return true;
    }
}
