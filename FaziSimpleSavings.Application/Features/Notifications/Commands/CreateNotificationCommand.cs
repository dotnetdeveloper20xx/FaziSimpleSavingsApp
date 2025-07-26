using MediatR;
using System;

namespace FaziSimpleSavings.Application.Notifications.Commands
{
    public class CreateNotificationCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Message { get; set; }

        public CreateNotificationCommand(Guid userId, string message)
        {
            UserId = userId;
            Message = message;
        }
    }
}
