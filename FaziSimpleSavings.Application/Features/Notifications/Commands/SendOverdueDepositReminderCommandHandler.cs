using Application.Interfaces;
using FaziSimpleSavings.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Notifications.Commands.SendOverdueDepositReminder;

public class SendOverdueDepositReminderCommandHandler : IRequestHandler<SendOverdueDepositReminderCommand, Guid>
{
    private readonly IAppDbContext _context;

    public SendOverdueDepositReminderCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(SendOverdueDepositReminderCommand request, CancellationToken cancellationToken)
    {
        var message = $"Your recurring deposit of £{request.Amount} for \"{request.GoalName}\" is overdue. Please review your savings goal.";

        // Check if a similar notification already exists
        var exists = await _context.Notifications.AnyAsync(n =>
            n.UserId == request.UserId &&
            n.Message == message &&
            !n.IsRead, cancellationToken);

        if (exists)
        {
            return Guid.Empty; // Notification already sent — skip
        }

        var notification = new Notification(request.UserId, message);
        await _context.Notifications.AddAsync(notification, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return notification.Id;
    }
}
