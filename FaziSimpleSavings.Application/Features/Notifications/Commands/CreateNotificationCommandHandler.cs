
using Application.Common.Security;
using Application.Interfaces;

using FaziSimpleSavings.Application.Notifications.Commands;
using FaziSimpleSavings.Core.Entities;
using MediatR;

namespace Application.Notifications.Commands.CreateNotification;

public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly IOwnershipValidator _ownershipValidator;

    public CreateNotificationCommandHandler(IAppDbContext context, IOwnershipValidator ownershipValidator)
    {
        _context = context;
        _ownershipValidator = ownershipValidator;
    }

    public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        // Optional: Validate user owns related resource (e.g., goal)
        // This is only necessary if you add GoalId in the request in the future

        // Create and persist the notification
        var notification = new Notification(request.UserId, request.Message);
        await _context.Notifications.AddAsync(notification, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return notification.Id;
    }
}
