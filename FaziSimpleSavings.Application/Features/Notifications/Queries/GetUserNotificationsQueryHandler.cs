
using Application.Interfaces;
using FaziSimpleSavings.Application.Dtos;
using FaziSimpleSavings.Application.Notifications.Queries;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FaziSimpleSavings.Application.Notifications.Handlers
{
    public class GetUserNotificationsQueryHandler : IRequestHandler<GetUserNotificationsQuery, List<NotificationDto>>
    {
        private readonly IAppDbContext _context;

        public GetUserNotificationsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<NotificationDto>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Notifications
                .Where(n => n.UserId == request.UserId)
                .OrderByDescending(n => n.NotificationDate)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    Message = n.Message,
                    NotificationDate = n.NotificationDate,
                    IsRead = n.IsRead
                })
                .ToListAsync(cancellationToken);
        }
    }
}
