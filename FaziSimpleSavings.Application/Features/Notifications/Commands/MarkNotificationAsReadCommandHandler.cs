using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Notifications.Commands.MarkNotificationAsRead;

public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, bool>
{
    private readonly IAppDbContext _context;

    public MarkNotificationAsReadCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(n =>
                n.Id == request.NotificationId &&
                n.UserId == request.UserId,
                cancellationToken);

        if (notification == null)
            return false;

        notification.MarkAsRead();

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
