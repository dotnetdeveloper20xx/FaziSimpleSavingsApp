using Application.Common.Security;
using Application.Interfaces;
using MediatR;


namespace Application.Notifications.Commands.MarkNotificationAsRead;

public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, bool>
{
    private readonly IAppDbContext _context;
    private readonly IOwnershipValidator _ownershipValidator;

    public MarkNotificationAsReadCommandHandler(IAppDbContext context, IOwnershipValidator ownershipValidator)
    {
        _context = context;
        _ownershipValidator = ownershipValidator;
    }

    public async Task<bool> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        var ownsNotification = await _ownershipValidator.UserOwnsNotification(request.UserId, request.NotificationId);
        if (!ownsNotification)
            return false;

        var notification = await _context.Notifications.FindAsync(new object[] { request.NotificationId }, cancellationToken);
        if (notification == null)
            return false;

        notification.MarkAsRead();
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
