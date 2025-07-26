using FaziSimpleSavings.Application.Dtos;
using MediatR;

namespace FaziSimpleSavings.Application.Notifications.Queries
{
    public class GetUserNotificationsQuery : IRequest<List<NotificationDto>>
    {
        public Guid UserId { get; }

        public GetUserNotificationsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
 
}
