using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using FaziSimpleSavings.Application.Notifications.Commands;
using FaziSimpleSavings.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FaziSimpleSavings.Application.Notifications.Handlers
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateNotificationCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification(request.UserId, request.Message);
            await _context.Notifications.AddAsync(notification, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return notification.Id;
        }
    }
}
